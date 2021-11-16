using System.Threading;
using System.Threading.Tasks;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Dao
{
    public class CategoryDao : DaoAsync<Category>
    {
        public CategoryDao(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<Category?> FindAsync(long id, CancellationToken token = default) =>
            await _context.Categories.Include(c => c.Subcategories)
                .FirstOrDefaultAsync(c => c.Id == id, token);
    }
}
