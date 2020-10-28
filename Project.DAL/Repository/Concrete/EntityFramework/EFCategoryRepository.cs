using Project.DAL.Repository.Abstract;
using Project.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.DAL.Repository.Concrete.EntityFramework
{
   public class EFCategoryRepository : EFGenericRepository<Category>, ICategoryRepository
    {
        public EFCategoryRepository(KuskonmazContext context) : base(context)
        {

        }
        public KuskonmazContext KuskonmazContext
        {
            get { return context as KuskonmazContext; }
        }

    }
}
