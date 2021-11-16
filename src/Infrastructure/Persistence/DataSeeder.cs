using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public sealed class DataSeeder
    {
        private readonly ApplicationDbContext _context;

        public DataSeeder(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> SeedDataAsync(CancellationToken token = default)
        {
            if (await _context.Categories.AnyAsync(token) || await _context.Subcategories.AnyAsync(token))
                return 0;
            var categories = new List<Category>
            {
                new() {Name = "Web app"},
                new() {Name = "Android app"}
            };
            var subcategories = new List<Subcategory>
            {
                new() {Name = "Database design", ParentCategory = categories[0]},
                new() {Name = "API design", ParentCategory = categories[0]},
                new() {Name = "UI design", ParentCategory = categories[1]}
            };

            await _context.Categories.AddRangeAsync(categories, token);
            await _context.Subcategories.AddRangeAsync(subcategories, token);
            
            return await _context.SaveChangesAsync(token);
        }
    }
}
