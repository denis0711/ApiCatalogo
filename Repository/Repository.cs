using ApiCatalogo.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ApiCatalogo.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly MeuDbContext _meuDbContext;

        public Repository(MeuDbContext meuDbContext)
        {
            _meuDbContext = meuDbContext;
        }
        public void Add(T entity)
        {
            _meuDbContext.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            _meuDbContext.Set<T>().Remove(entity);
        }

        public IQueryable<T> Get()
        {
            return _meuDbContext.Set<T>().AsNoTracking();
        }

        public T GetBydId(Expression<Func<T, bool>> predicate)
        {
            return _meuDbContext.Set<T>().SingleOrDefault(predicate);
        }

        public void Update(T entity)
        {
            _meuDbContext.Entry(entity).State = EntityState.Modified;
            _meuDbContext.Set<T>().Update(entity);
        }
    }
}
