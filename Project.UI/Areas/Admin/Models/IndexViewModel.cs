using Project.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.UI.Areas.Admin.Models
{
    public class IndexViewModel
    {
        public Post post { get; set; }
        public List<Post> Posts { get; set; }
        public List<PostCategory> SameCat { get; set; }
        public List<PostTag> SameTag { get; set; }
        public List<Category> Categories { get; set; }
        public List<Tag> Tags { get; set; }
        public List<PostTag> postTags { get; set; }
        public List<Comment> Comments { get; set; }
        public List<About> Abouts { get; set; }
        public List<Post> MostViews { get; set; }
        public List<Post> MostComments { get; set; }
        public List<Post> MostLikes { get; set; }
        public List<Post> dataAdded { get; set; }
        public int totalPageCount { get; set; }
        public int currentPage { get; internal set; }
    }
}
