using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Project.Entities
{
   public class Comment
    {
        public int commentID { get; set; }
        [Required]
        public string commentContent { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int Likes { get; set; }
        public int disLikes { get; set; }
        public int userId { get; set; }
        public User User { get; set; }//her yorumun user ı vardır
        public int postId { get; set; }
        public Post Post { get; set; }//her yorumun oost u vardır
    }
}
