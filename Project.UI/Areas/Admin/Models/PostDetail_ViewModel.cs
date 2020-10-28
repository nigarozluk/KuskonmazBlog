using Project.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.UI.Areas.Admin.Models
{
    public class PostDetail_ViewModel
    {
        public Post post { get; set; }
        public List<Category> Categories { get; set; }
        public List<Tag> Tags { get; set; }
        public List<int> SelectedCategories { get; set; }
        public List<int> SelectedTags { get; set; }
        public int Likes { get; set; }

    }
}
