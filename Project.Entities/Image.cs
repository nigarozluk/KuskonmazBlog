using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Entities
{
    public class Image
    {
        public int imageId { get; set; }
        public string imageName { get; set; }
        public int postId { get; set; }
        public Post Post { get; set; }
    }
}
