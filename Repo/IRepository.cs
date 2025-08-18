using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Repo
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Delbyid(int id);
        void Delbystringid(string id);
        T Find(Expression<Func<T, bool>> Name);
        IQueryable<T> GetQueryable();
    }
}