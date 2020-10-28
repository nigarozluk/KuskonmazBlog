using System;
using System.Collections.Generic;
using System.Text;

namespace Project.DAL.Repository.Abstract
{
   public  interface IUnitOfWork: IDisposable
    {
        IPostRepository Posts { get; }
        ICategoryRepository Categories { get; }
        ITagRepository Tags { get; }
        ICommentRepository Comments { get; }
        IUserRepository Users { get; }
        IPostCategoryRepository PostCategories { get; }
        IPostTagRepository PostTags { get; }
        IAboutRepository Abouts { get; }
        IContactRepository Contacts { get; }
        IAdminRepository Admins { get; }

        int SaveChanges();
    }
}
