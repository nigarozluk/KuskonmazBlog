using Project.DAL.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.DAL.Repository.Concrete.EntityFramework
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private readonly KuskonmazContext context;
        public EFUnitOfWork(KuskonmazContext _context)
        {
            context = _context ?? throw new ArgumentNullException("dbcontext can not be null");
        }
        private IPostRepository _posts;
        private ICategoryRepository _categories;
        private ITagRepository _tags;
        private IUserRepository _users;
        private ICommentRepository _comments;
        private IPostCategoryRepository _postCategories;
        private IPostTagRepository _postTags;
        private IAboutRepository _abouts;
        private IContactRepository _contacts;
        private IAdminRepository _admins;

        public IPostRepository Posts
        {
            get
            {
                return _posts ?? (_posts = new EFPostRepository(context));
            }
        }
        public ICategoryRepository Categories
        {
            get
            {
                return _categories ?? (_categories = new EFCategoryRepository(context));
            }
        }
        public IUserRepository Users
        {
            get
            {
                return _users ?? (_users = new EFUserRepository(context));
            }
        }
        public ICommentRepository Comments
        {
            get
            {
                return _comments ?? (_comments = new EFCommentRepository(context));
            }
        }
        public ITagRepository Tags
        {
            get
            {
                return _tags ?? (_tags = new EFTagRepository(context));
            }
        }
        public IPostCategoryRepository PostCategories
        {
            get
            {
                return _postCategories ?? (_postCategories = new EFPostCategoryRepository(context));
            }
        }
        public IPostTagRepository PostTags
        {
            get
            {
                return _postTags ?? (_postTags = new EFPostTagRepository(context));
            }
        }
        public IAboutRepository Abouts
        {
            get
            {
                return _abouts ?? (_abouts = new EFAboutRepository(context));
            }
        }
        public IContactRepository Contacts
        {
            get
            {
                return _contacts ?? (_contacts = new EFContactRepository(context));
            }
        }
        public IAdminRepository Admins
        {
            get
            {
                return _admins ?? (_admins = new EFAdminRepository(context));
            }
        }




        public void Dispose()
        {
            context.Dispose();
        }

        public int SaveChanges()
        {
            try
            {
                return context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
