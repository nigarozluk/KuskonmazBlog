using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Project.Entities
{
    public class PostCategory
    {
        public int categoryId { get; set; }
        public Category Category { get; set; }
        public int postId { get; set; }
        public Post Post{ get; set; }
    }
}
