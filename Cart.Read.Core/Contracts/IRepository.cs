﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Cart.Read.Core.Contracts
{
    public interface IRepository<TEntity> where TEntity : IAggregateRoot
    {
        void Add(TEntity entity);

        void Remove(TEntity entity);

        void Update(TEntity entity);
        Task<TEntity> GetByIdAsync(object id);


        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> GetAllAsync<TProperty>(Expression<Func<TEntity, TProperty>> include);


        Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
    }
}
