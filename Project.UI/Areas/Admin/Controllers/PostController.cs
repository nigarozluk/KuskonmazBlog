using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.DAL.Repository.Abstract;
using Project.Entities;
using Project.UI.Areas.Admin.Models;
using Project.UI.Areas.Filter;

namespace Project.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AdminRole]
    public class PostController : Controller
    {
        private IUnitOfWork unitOfWork;
        public PostController(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }
        public IActionResult Index()
        {
            var postModel = unitOfWork.Posts.GetAll().Include(k => k.Comments).ToList();
            Dictionary<int, int> postCommentCoutn = new Dictionary<int, int>();
            foreach (var post in postModel)
            {
                int _count = unitOfWork.Comments.Count(k => k.postId == post.postId);
                postCommentCoutn.Add(post.postId, _count);
            }


            ViewBag.count = postCommentCoutn;

            return View(postModel);

        }
        [HttpGet]
        public IActionResult AddPost()
        {
            ViewBag.CategoryList = unitOfWork.Categories.GetAll().ToList();
            ViewBag.TagList = unitOfWork.Tags.GetAll().ToList();
            return View();
        }

        [HttpPost]
        public IActionResult AddPost(Post entity, IFormFile file, int[] categories, string[] tags)
        {

            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "adminmaster\\images", file.FileName);
                    entity.postImage = file.FileName;

                }
                entity.dateAdded = DateTime.Now;
                unitOfWork.Posts.Add(entity);
                unitOfWork.SaveChanges();
                //return RedirectToAction("Index");

                //Bu Post ID'si İle Categorileme Yapalım:
                foreach (int cat in categories)
                {
                    Category dbCat = unitOfWork.Categories.Get(k => k.categoryId == cat);
                    if (dbCat == null)
                    {
                        //seeddata içerisindeki kategoriler ile sınırlı
                        //dbCat = new Category();
                        //dbCat.categoryName = cat.ToLower().Trim();
                        //unitOfWork.Categories.Add(dbCat);
                        //unitOfWork.SaveChanges();
                    }
                    else
                    {
                        //category database de varsa categoryid ile postid eşitle:
                        unitOfWork.PostCategories.Add(new PostCategory
                        {
                            categoryId = dbCat.categoryId,
                            postId = entity.postId
                        });
                        unitOfWork.SaveChanges();
                    }
                    unitOfWork.SaveChanges();
                }
                //Bu Post ID'si İle Etiketleme Yapalım:
                foreach (string tag in tags)
                {
                    Tag dbTag = unitOfWork.Tags.Get(k => k.tagName.ToLower() == tag.ToLower().Trim());
                    if (dbTag == null)
                    {
                        //etiket databasede yoksa:
                        dbTag = new Tag();
                        dbTag.tagName = tag.ToLower().Trim();
                        unitOfWork.Tags.Add(dbTag);
                        unitOfWork.SaveChanges();
                    }
                    PostTag pTag = unitOfWork.PostTags.Get(k => k.postId == entity.postId && k.tagId == dbTag.tagId);
                    if (pTag == null)
                    {
                        //etiket varsa ekle ve tagid ile postid eşitle:
                        unitOfWork.PostTags.Add(new PostTag
                        {
                            tagId = dbTag.tagId,
                            postId = entity.postId
                        });
                        unitOfWork.SaveChanges();
                    }

                }

            }
            return RedirectToAction("Index");
        }

        public IActionResult PostDetail(int id)
        {
            //return View(unitOfWork.Posts.Get(id));

            PostDetail_ViewModel model = new PostDetail_ViewModel();
            model.post = unitOfWork.Posts.Find(k => k.postId == id).Include(k => k.Comments).ThenInclude(k => k.User).FirstOrDefault();
            model.Categories = unitOfWork.PostCategories.Find(k => k.postId == model.post.postId)
                .Select(k => k.Category).ToList();
            model.Tags = unitOfWork.PostTags.Find(k => k.postId == model.post.postId).Select(k => k.Tag).ToList();

            //GÖRÜNTÜLENME SAYISI
            model.post.Views++;
            unitOfWork.SaveChanges();
            return View(model);
        }

        public IActionResult EditPost(int id)
        {
            PostDetail_ViewModel model = new PostDetail_ViewModel();
            model.post = unitOfWork.Posts.FirsOrDefault(k => k.postId == id);
            model.Categories = unitOfWork.Categories.GetAll().ToList();
            model.SelectedCategories = unitOfWork.PostCategories.Find(k => k.postId == id).Select(k => k.categoryId).ToList();
            model.Tags = unitOfWork.Tags.GetAll().ToList();
            model.SelectedTags = unitOfWork.PostTags.Find(k => k.postId == id).Select(k => k.tagId).ToList();
            return View(model);
        }
        [HttpPost]
        public IActionResult EditPost(PostDetail_ViewModel model, IFormFile file, int[] categories, string[] tags)
        {
            if (ModelState.IsValid)
            {
                var entity = model.post;

                if (file != null)
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "adminmaster\\images", file.FileName);
                    entity.postImage = file.FileName;
                }
                entity.dateAdded = DateTime.Now;
                unitOfWork.Posts.Edit(entity);
                unitOfWork.SaveChanges();
                //Bu Post ID'si İle Categorileme Yapalım:
                foreach (int cat in categories)
                {
                    //bu kategori database de var mı
                    Category dbCat = unitOfWork.Categories.Get(k => k.categoryId == cat);

                    if (dbCat != null)
                    {
                        //database de bu kategori ile ilişkili bir post var mı
                        var dbPostCat = unitOfWork.PostCategories.Get(k => k.postId == entity.postId);

                        // 1 - hiç yok >> ekle
                        if (dbPostCat == null)
                        {
                            unitOfWork.PostCategories.Add(new PostCategory { categoryId = cat, postId = entity.postId });
                            unitOfWork.SaveChanges();
                        }
                        // 2 - varsa >> cat id-ler farklıysa >> eskisini sil, yeni ekle
                        else if (dbPostCat != null && dbPostCat.categoryId != cat)
                        {
                            unitOfWork.PostCategories.Delete(dbPostCat);
                            unitOfWork.PostCategories.Add(new PostCategory { categoryId = cat, postId = entity.postId });
                            unitOfWork.SaveChanges();
                        }
                        // 3 - varsa >> cat eşitse >> hiçbir yapma
                    }
                    else
                    {
                        //seeddata dışından bir kategori eklenmeye çalışılırsa
                        return RedirectToAction("Error");
                    }
                }
                //Bu Post ID'si İle Etiketleme Yapalım:
                foreach (string tag in tags)
                {
                    Tag dbTag = unitOfWork.Tags.Get(k => k.tagName == tag.ToLower().Trim());
                    if (dbTag == null)
                    {
                        //etiket database de yoksa yeni etiket  oluştur 
                        dbTag = new Tag();
                        dbTag.tagName = tag.ToLower().Trim();
                        unitOfWork.Tags.Add(dbTag);
                        unitOfWork.SaveChanges();
                    }

                    PostTag pTag = unitOfWork.PostTags.Get(k => k.postId == entity.postId && k.tagId == dbTag.tagId);
                    if (pTag == null)
                    {
                        //etiket varsa ekle ve tagid ile postid eşitle:
                        unitOfWork.PostTags.Add(new PostTag
                        {
                            tagId = dbTag.tagId,
                            postId = entity.postId
                        });
                        unitOfWork.SaveChanges();
                    }

                }
                return RedirectToAction("Index");

            }
            return RedirectToAction("Index");
        }

        public IActionResult DeletePost(int id)
        {
            var post = unitOfWork.Posts.FirsOrDefault(k => k.postId == id);
            unitOfWork.Posts.Delete(post);
            unitOfWork.SaveChanges();
            return RedirectToAction("Index");
        }

        public string Like(int id)
        {
            Post post = unitOfWork.Posts.FirsOrDefault(k => k.postId == id);
            post.Likes++;
            unitOfWork.SaveChanges();
            return post.Likes.ToString();
        }

        [HttpPost]
        public ActionResult CreateComment(Comment comment)
        {
            comment.ReleaseDate = DateTime.Now;
            unitOfWork.Comments.Add(comment);
            unitOfWork.SaveChanges();
            return RedirectToAction("PostDetail", new { id = comment.postId });


        }
        public ActionResult DeleteComment(int id)
        {
            var comment = unitOfWork.Comments.FirsOrDefault(k => k.commentID == id);
            unitOfWork.Comments.Delete(comment);
            unitOfWork.SaveChanges();
            return RedirectToAction("PostDetail", new { id = comment.postId });


        }
    }
}