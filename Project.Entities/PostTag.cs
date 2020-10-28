using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Entities
{
    public class PostTag
    {
        public int tagId { get; set; }
        public Tag Tag { get; set; }
        public int postId { get; set; }
        public Post Post { get; set; }
    }
}
