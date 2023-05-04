using System.Linq.Expressions;
using Member.Infrastructure.Abstraction.Interfaces;
using Microsoft.EntityFrameworkCore;
using Member.Context;

namespace Member.Infrastructure.Repostory
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly MemberContext _memeberContext;
        private readonly DbSet<TEntity> _dbset;

        public Repository(MemberContext db)
        {
            _memeberContext = db;
            _dbset = _memeberContext.Set<TEntity>();
            
        }

        public IQueryable<TEntity> GetAll()
        {
            return _dbset;
        }

        public async Task<bool> AddAsync(TEntity entity) 
        {
            await _memeberContext.AddAsync(entity);
            return true;
        }
        

        
        public async Task SaveChangesAsync()
        {
            await _memeberContext.SaveChangesAsync();
        }
        public  TEntity? UpdateAsync(TEntity entity)
        {
            return  _dbset.Update(entity).Entity;
        }
        public async Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate,
                                   CancellationToken cancellationToken)
        {
            return await _dbset.FirstOrDefaultAsync(predicate, cancellationToken);
        }

        public async Task<TEntity?> GetByIdAsync(int id)
        {

            
            return await _dbset.FindAsync(id);
        }

        public async Task<TEntity?> FindAsync(TEntity entity)
        {
            return await _dbset.FindAsync(entity);
        }
    }
}
