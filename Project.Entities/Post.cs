using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Project.Entities
{
    public class Post
    {
        public int postId { get; set; }
        [Required]
        public string postTitle { get; set; }
        public string postContent { get; set; }
        public string postImage { get; set; }
        public string postDescription { get; set; }
        public DateTime dateAdded { get; set; }
        public int Views { get; set; } //Post Görüntülenme Sayısı
        public int Likes { get; set; } // post Beğeni Sayısı
        public List<Comment> Comments { get; set; } //Her Makalenin Birden Fazla Yorumu Olabilir
        public List<PostCategory> PostCategories { get; set; }
       
        public List<PostTag> PostTags { get; set; }
        public List<Image> Images { get; set; }
    }
}
