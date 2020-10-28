using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Project.Entities
{
    public class Tag
    {
        public int tagId { get; set; }
        
        public string tagName { get; set; }
        public List<PostTag> PostTags { get; set; }
    }
}
