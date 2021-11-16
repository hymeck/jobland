using System.Threading;
using System.Threading.Tasks;
using Application.Dao;
using Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Dao
{
    public class DaoAsync<TEntity> : IDaoAsync<TEntity> where TEntity : Entity
    {
        protected readonly ApplicationDbContext _context;

        public DaoAsync(ApplicationDbContext context)
        {
            _context = context;
        }

        public virtual async Task<TEntity?> FindAsync(long id, CancellationToken token = default) =>
            await _context
                .Set<TEntity>()
                .FirstOrDefaultAsync(e => e.Id == id, token);

        public async Task<TEntity?> AddAsync(TEntity entity, CancellationToken token = default)
        {
            await _context
                .Set<TEntity>()
                .AddAsync(entity, token);
            await _context.SaveChangesAsync(token);
            return entity;
        }
    }
}
