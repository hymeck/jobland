using System.Threading;
using System.Threading.Tasks;
using Domain;

namespace Application.Dao
{
    public interface IWorkDao : IDaoAsync<Work>
    {
        public Task<Work?> FindByNameAsync(string workName, CancellationToken token = default);
    }
}
