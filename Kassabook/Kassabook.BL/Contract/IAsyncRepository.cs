using System;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace Kassabook.BL.Contract
{
	public interface IAsyncRepository<TEntity> where TEntity : class
    {
        Task<TEntity?> GetById(Guid id);
        Task<IEnumerable<TEntity?>> GetAll();
        Task<TEntity?> Find(Expression<Func<TEntity, bool>> predicate);
        Task<EntityEntry<TEntity>> Add(TEntity entity);
        Task AddRange(IEnumerable<TEntity> entites);
        Task<int> Remove(TEntity entity);
        Task<int> RemoveRange(IEnumerable<TEntity> entities);
        Task Update(TEntity entity);
        Task<int> Complete();
    }
}

