using System.Threading;
using System.Threading.Tasks;
using Domain.Common;

namespace Application.Dao
{
    public interface IDaoAsync<TEntity> where TEntity : Entity
    {
        public Task<TEntity?> FindAsync(long id, CancellationToken token = default);
        public Task<TEntity?> AddAsync(TEntity entity, CancellationToken token = default);
    }
}
