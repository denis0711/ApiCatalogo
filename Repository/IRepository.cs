﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ApiCatalogo.Repository
{
    public interface IRepository<T> 
    {
        IQueryable<T> Get();
        T GetBydId(Expression<Func<T, bool>> predicate);

        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);


          
    }
}
