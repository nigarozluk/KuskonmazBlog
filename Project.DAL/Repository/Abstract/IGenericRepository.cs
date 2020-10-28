using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Project.DAL.Repository.Abstract
{
    public interface EFGenericRepository<T> where T : class
    {
        T Get(int id);
        T Get(Expression<Func<T, bool>> predicate);
        T FirsOrDefault(Expression<Func<T, bool>> predicate);
        IQueryable<T> GetAll();
        IQueryable<T> Find(Expression<Func<T, bool>> predicate);
        int Count(Expression<Func<T, bool>> predicate = null);
        void Add(T entity);
        void Delete(T entity);
        void Edit(T entity);
        void Save();
    }
}
