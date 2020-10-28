using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.DAL.Repository.Abstract;
using Project.Entities;
using Project.UI.Areas.Admin.Models;

namespace Project.UI.Controllers
{
    public class HomeController : Controller
    {
        private IUnitOfWork unitOfWork;
        public HomeController(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }
        public IActionResult Index(int page = 1)
        {
            IndexViewModel vm = new IndexViewModel();

            // Pagination
            int takenPost = 4;
            int skip = (page - 1) * takenPost;

            int postCount = unitOfWork.Posts.GetAll().Count();
            int totalCount = postCount % takenPost == 0 ? postCount / takenPost : (postCount / takenPost) + 1;

            vm.dataAdded = unitOfWork.Posts.GetAll().OrderByDescending(k => k.dateAdded).Skip(skip).Take(takenPost).ToList();
            vm.totalPageCount = totalCount;
            vm.currentPage = page;

            vm.Categories = unitOfWork.Categories.GetAll().Include(k => k.PostCategories).OrderByDescending(k => k.PostCategories.Count()).Take(5).ToList();
            vm.Posts = unitOfWork.Posts.GetAll().ToList();
            vm.Tags = unitOfWork.Tags.GetAll().Include(k => k.PostTags).OrderByDescending(k => k.PostTags.Count()).Take(10).ToList();
            vm.MostViews = unitOfWork.Posts.GetAll().OrderByDescending(k => k.Views).Take(5).ToList();
            vm.MostLikes = unitOfWork.Posts.GetAll().Include(k => k.PostCategories).OrderByDescending(k => k.Likes).Take(5).ToList();
            vm.MostComments = unitOfWork.Posts.GetAll().Include(k => k.Comments).ToList();
            vm.Abouts = unitOfWork.Abouts.GetAll().ToList();
            Dictionary<int, int> postCommentCoutn = new Dictionary<int, int>();
            foreach (var post in vm.MostComments)
            {
                int _count = unitOfWork.Comments.Count(k => k.postId == post.postId);
                postCommentCoutn.Add(post.postId, _count);
            }
            ViewBag.count = postCommentCoutn;

            Dictionary<int, List<string>> postCat = new Dictionary<int, List<string>>();
            foreach (var item in vm.Posts)
            {
                var catlist = unitOfWork.PostCategories.GetAll().Where(k => k.postId == item.postId).Select(k => k.Category.categoryName).ToList();
                postCat.Add(item.postId, catlist);
            }

            ViewBag.cat = postCat;

            return View(vm);
        }
        public IActionResult SinglePost(int id)
        {
            IndexViewModel vm = new IndexViewModel();
            vm.Categories = unitOfWork.Categories.GetAll().Include(k => k.PostCategories).OrderByDescending(k => k.PostCategories.Count()).Take(5).ToList();
            vm.Posts = unitOfWork.Posts.GetAll().ToList();
            vm.post = unitOfWork.Posts.Find(k => k.postId == id).Include(k => k.Comments).ThenInclude(k => k.User).FirstOrDefault();
            var catid = unitOfWork.PostCategories.Find(k => k.postId == id).Select(k => k.categoryId).FirstOrDefault();
            vm.SameCat = unitOfWork.PostCategories.GetAll().Include(k => k.Post).Where(k => k.categoryId == catid).OrderByDescending(k => k.categoryId).Take(2).ToList();
            vm.Tags = unitOfWork.Tags.GetAll().Include(k => k.PostTags).OrderByDescending(k => k.PostTags.Count()).Take(10).ToList();
            vm.dataAdded = unitOfWork.Posts.GetAll().OrderByDescending(k => k.dateAdded).Take(5).ToList();
            vm.MostViews = unitOfWork.Posts.GetAll().OrderByDescending(k => k.Views).Take(5).ToList();
            vm.MostLikes = unitOfWork.Posts.GetAll().Include(k => k.PostCategories).OrderByDescending(k => k.Likes).Take(5).ToList();
            vm.MostComments = unitOfWork.Posts.GetAll().Include(k => k.Comments).ToList();
            vm.Abouts = unitOfWork.Abouts.GetAll().ToList();
            Dictionary<int, int> postCommentCoutn = new Dictionary<int, int>();
            foreach (var post in vm.MostComments)
            {
                int _count = unitOfWork.Comments.Count(k => k.postId == post.postId);
                postCommentCoutn.Add(post.postId, _count);
            }
            ViewBag.count = postCommentCoutn;

            Dictionary<int, List<string>> postCat = new Dictionary<int, List<string>>();
            foreach (var item in vm.Posts)
            {
                var catlist = unitOfWork.PostCategories.GetAll().Where(k => k.postId == item.postId).Select(k => k.Category.categoryName).ToList();
                postCat.Add(item.postId, catlist);
            }
            ViewBag.cat = postCat;
            //GÖRÜNTÜLENME SAYISI
            vm.post.Views++;
            unitOfWork.SaveChanges();
            return View(vm);
        }
        public IActionResult About()
        {
            IndexViewModel vm = new IndexViewModel();
            vm.Categories = unitOfWork.Categories.GetAll().Include(k => k.PostCategories).OrderByDescending(k => k.PostCategories.Count()).Take(5).ToList();
            ViewBag.About = unitOfWork.Abouts.GetAll().ToList();
            return View(vm);
        }
        public IActionResult Contact()
        {
            IndexViewModel vm = new IndexViewModel();
            vm.Categories = unitOfWork.Categories.GetAll().Include(k => k.PostCategories).OrderByDescending(k => k.PostCategories.Count()).Take(5).ToList();
            ViewBag.Contact = unitOfWork.Contacts.GetAll().ToList();
            return View(vm);
        }
        public string Like(int id)
        {
            var post = unitOfWork.Posts.FirsOrDefault(k => k.postId == id);
            post.Likes++;
            unitOfWork.SaveChanges();
            return post.Likes.ToString();
        }
        public string CommentLike(int id)
        {
            var comment = unitOfWork.Comments.FirsOrDefault(k => k.commentID == id);
            comment.Likes++;
            unitOfWork.SaveChanges();
            return comment.Likes.ToString();
        }
        public string CommentDisLike(int id)
        {
            var comment = unitOfWork.Comments.FirsOrDefault(k => k.commentID == id);
            comment.disLikes++;
            unitOfWork.SaveChanges();
            return comment.disLikes.ToString();
        }

        public ActionResult AddComment(string commentContent, User user, int postId)
        {
            if (ModelState.IsValid)
            {
                //user databasede yoksa:
                var _user = new User();
                _user.userName = user.userName;
                _user.userMail = user.userMail;
                unitOfWork.Users.Add(_user);
                unitOfWork.SaveChanges();
                var _comment = new Comment();
                _comment.userId = _user.userID;
                _comment.ReleaseDate = DateTime.Now;
                _comment.commentContent = commentContent;
                _comment.postId = postId;
                unitOfWork.Comments.Add(_comment);
                unitOfWork.SaveChanges();
            }

            return RedirectToAction("SinglePost", new { id =postId });


        }
        public ActionResult CategoryPostList(int id)
        {
            IndexViewModel vm = new IndexViewModel();
            vm.Categories = unitOfWork.Categories.GetAll().Include(k => k.PostCategories).OrderByDescending(k => k.PostCategories.Count()).Take(5).ToList();
            var catid = unitOfWork.PostCategories.Find(k => k.categoryId == id).Select(k => k.categoryId).FirstOrDefault();
            vm.SameCat = unitOfWork.PostCategories.GetAll().Include(k => k.Post).Where(k => k.categoryId == catid).OrderByDescending(k => k.categoryId).ToList();
            vm.MostComments = unitOfWork.Posts.GetAll().Include(k => k.Comments).ToList();
            vm.Posts = unitOfWork.Posts.GetAll().ToList();
            Dictionary<int, int> postCommentCoutn = new Dictionary<int, int>();
            foreach (var post in vm.MostComments)
            {
                int _count = unitOfWork.Comments.Count(k => k.postId == post.postId);
                postCommentCoutn.Add(post.postId, _count);
            }
            ViewBag.count = postCommentCoutn;

            Dictionary<int, List<string>> postCat = new Dictionary<int, List<string>>();
            foreach (var item in vm.Posts)
            {
                var catlist = unitOfWork.PostCategories.GetAll().Where(k => k.postId == item.postId).Select(k => k.Category.categoryName).ToList();
                postCat.Add(item.postId, catlist);
            }
            ViewBag.cat = postCat;
            ViewBag.catname=unitOfWork.Categories.GetAll().Where(k=>k.categoryId==id).Select(k => k.categoryName).ToList();
            return View(vm);
        }

        public ActionResult TagPostList(int id)
        {
            IndexViewModel vm = new IndexViewModel();
            vm.Categories = unitOfWork.Categories.GetAll().Include(k => k.PostCategories).OrderByDescending(k => k.PostCategories.Count()).Take(5).ToList();
            var tagid = unitOfWork.PostTags.Find(k => k.tagId == id).Select(k => k.tagId).FirstOrDefault();
            vm.SameTag = unitOfWork.PostTags.GetAll().Include(k => k.Post).Where(k => k.tagId == tagid).OrderByDescending(k => k.tagId).ToList();
            vm.MostComments = unitOfWork.Posts.GetAll().Include(k => k.Comments).ToList();
            vm.Posts = unitOfWork.Posts.GetAll().ToList();
            Dictionary<int, int> postCommentCoutn = new Dictionary<int, int>();
            foreach (var post in vm.MostComments)
            {
                int _count = unitOfWork.Comments.Count(k => k.postId == post.postId);
                postCommentCoutn.Add(post.postId, _count);
            }
            ViewBag.count = postCommentCoutn;

            Dictionary<int, List<string>> postCat = new Dictionary<int, List<string>>();
            foreach (var item in vm.Posts)
            {
                var catlist = unitOfWork.PostCategories.GetAll().Where(k => k.postId == item.postId).Select(k => k.Category.categoryName).ToList();
                postCat.Add(item.postId, catlist);
            }
            ViewBag.cat = postCat;
            ViewBag.tagname = unitOfWork.Tags.GetAll().Where(k => k.tagId == id).Select(k => k.tagName).ToList();
            return View(vm);
        }
    }
}