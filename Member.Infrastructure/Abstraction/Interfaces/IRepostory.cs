using System;
using System.Linq.Expressions;

namespace Member.Infrastructure.Abstraction.Interfaces
{
	public interface IRepository<TEntity>
	{
		Task<bool> AddAsync(TEntity entity);
		Task SaveChangesAsync();
		Task<TEntity> FindAsync(TEntity entity);
		Task<TEntity> GetByIdAsync(int id);
        IQueryable<TEntity> GetAll();
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate,
                                   CancellationToken cancellationToken = default);
		TEntity UpdateAsync(TEntity entity);
    }
}

