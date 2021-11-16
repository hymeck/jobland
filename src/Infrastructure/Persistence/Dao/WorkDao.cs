using System.Threading;
using System.Threading.Tasks;
using Application.Dao;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Dao
{
    public class WorkDao : DaoAsync<Work>, IWorkDao
    {
        public WorkDao(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<Work?> FindAsync(long id, CancellationToken token = default) =>
            await _context.Works
                .Include(w => w.Category)
                .Include(w => w.Subcategory)
                .FirstOrDefaultAsync(w => w.Id == id, token);

        public async Task<Work?> FindByNameAsync(string workName, CancellationToken token = default) => 
            await _context.Works.Include(w => w.Category)
                .FirstOrDefaultAsync(e => e.Name == workName, token);
    }
}
