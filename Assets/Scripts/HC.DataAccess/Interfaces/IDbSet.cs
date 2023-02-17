using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IDbSet<TEntity> where TEntity : class, IDbEntity
    {
        Task<TEntity> FirstOrDefault(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> Create(TEntity newEntity);

        Task<TEntity> Get(int id);

        Task<IReadOnlyCollection<TEntity>> All();

        Task Update(TEntity entity);

        Task EnsureCreated();
    }
}