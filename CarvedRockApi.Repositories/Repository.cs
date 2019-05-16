using CarvedRock.Api.Data;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CarvedRockApi.Repositories
{
    public class Repository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class where TKey : IEquatable<TKey>
    {
        private readonly CarvedRockDbContext _context;

        public Repository(CarvedRockDbContext context)
        {
            _context = context;
        }


        public Task<IQueryable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            return predicate == null
                ? Task.FromResult(_context.Set<TEntity>().AsQueryable())
                : Task.FromResult(_context.Set<TEntity>().Where(predicate));
        }

        public Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Task.FromResult(_context.Set<TEntity>().FirstOrDefault(predicate));
        }

        public Task<TEntity> GetAsync(TKey id)
        {
            return _context.FindAsync<TEntity>(id);
        }
    }
}
