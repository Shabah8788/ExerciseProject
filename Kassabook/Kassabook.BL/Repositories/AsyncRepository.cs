using System;
using Kassabook.BL.Contract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;
using Kassabook.DL;

namespace Kassabook.BL.Repositories
{
	public class AsyncRepository<TEntity> : IAsyncRepository<TEntity> where TEntity : class
    {
        protected readonly KassabookDbContext _context;

        public AsyncRepository(KassabookDbContext context)
        {
            _context = context;
        }

        public virtual async Task<TEntity?> GetById(Guid id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public virtual async Task<IEnumerable<TEntity?>> GetAll()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public virtual async Task<TEntity?> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return await _context.Set<TEntity>().FirstOrDefaultAsync(predicate);
        }

        public virtual async Task<IEnumerable<TEntity>> FindAll(Expression<Func<TEntity, bool>> predicate)
        {
            return await _context.Set<TEntity>().Where(predicate).ToListAsync();
        }

        public virtual async Task<EntityEntry<TEntity>> Add(TEntity entity)
        {
            var obj = await _context.Set<TEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();

            return obj;
        }

        public virtual async Task AddRange(IEnumerable<TEntity> entites)
        {
            await _context.Set<TEntity>().AddRangeAsync(entites);

            await _context.SaveChangesAsync();
        }

        public virtual async Task<int> Remove(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);

            return await _context.SaveChangesAsync();
        }

        public virtual async Task<int> RemoveRange(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().RemoveRange(entities);

            return await _context.SaveChangesAsync();
        }
        public virtual async Task Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }

        public virtual async Task<int> Complete()
        {
            return await _context.SaveChangesAsync();
        }

    }
}

