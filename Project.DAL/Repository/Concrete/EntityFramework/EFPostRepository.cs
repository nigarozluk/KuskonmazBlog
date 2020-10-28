using Project.DAL.Repository.Abstract;
using Project.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project.DAL.Repository.Concrete.EntityFramework
{
    public class EFPostRepository : EFGenericRepository<Post>, IPostRepository
    {
        public EFPostRepository(KuskonmazContext context) : base(context)
        {

        }
        public KuskonmazContext KuskonmazContext
        {
            get { return context as KuskonmazContext; }
        }

     
    }
}
