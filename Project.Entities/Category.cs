using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Project.Entities
{
    public class Category
    {
        public int categoryId { get; set; }
       
        public string categoryName { get; set; }
        public List<PostCategory> PostCategories { get; set; }
    }
}
