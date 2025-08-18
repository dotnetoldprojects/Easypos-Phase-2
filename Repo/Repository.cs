using Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repo
{
    public class Repository<T> : IRepository<T> where T : class
    {
        EasyposEntities _Dbc;
        public Repository(EasyposEntities Dbc)
        {
            _Dbc = Dbc;
        }
        public IEnumerable<T> GetAll()
        {
            return _Dbc.Set<T>().ToList();
        }
        public T Get(int id)
        {
            return _Dbc.Set<T>().Find(id);
        }
        public void Insert(T entity)
        {
            _Dbc.Set<T>().Add(entity);
        }
        public void Update(T entity)
        {
            _Dbc.Set<T>().AddOrUpdate(entity);
        }
        public void Delbyid(int id)
        {
            var Entdel = _Dbc.Set<T>().Find(id);
            _Dbc.Entry(Entdel).State = EntityState.Deleted;
        }
        public void Delete(T entity)
        {
            _Dbc.Set<T>().Attach(entity);
            _Dbc.Entry(entity).State = EntityState.Deleted;
        }
        public T Find(Expression<Func<T, bool>> Name)
        {
            return _Dbc.Set<T>().SingleOrDefault(Name);
        }
        public IQueryable<T> GetQueryable()
        {
            return _Dbc.Set<T>();
        }

        public void Delbystringid(string id)
        {
            var Entdel = _Dbc.Set<T>().Find(id);
            _Dbc.Entry(Entdel).State = EntityState.Deleted;
        }
    }
}
