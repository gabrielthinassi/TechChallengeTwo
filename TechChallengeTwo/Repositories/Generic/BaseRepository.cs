using Microsoft.EntityFrameworkCore;
using profilefinder_back.Repositories.Interfaces;
using System.Linq.Expressions;
using TechChallengeTwo.Data;
using TechChallengeTwo.Models.Entities;

namespace profilefinder_back.Repositories.Implementations
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        private ApplicationDbContext _context;
        private DbSet<T> dataset;

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
            dataset = _context.Set<T>();
        }

        public async Task<List<T>> FindAsync(Expression<Func<T, bool>> filter = null)
        {
            var query = dataset.AsQueryable();

            if (filter != null)
                query = query.Where(filter).AsNoTracking();

            return await query
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<T> FindByIdAsync(long id)
        {
            return await dataset.SingleOrDefaultAsync(p => p.Id.Equals(id));
        }

        public async Task<T> CreateAsync(T item)
        {
            try
            {
                dataset.Add(item);
                await _context.SaveChangesAsync();
                return item;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<T> UpdateAsync(T item)
        {
            var result = await dataset.SingleOrDefaultAsync(p => p.Id.Equals(item.Id));
            if (result != null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(item);
                    await _context.SaveChangesAsync();
                    return result;
                }
                catch (Exception)
                {
                    throw;
                }
            }
            else
            {
                return null;
            }
        }

        public async Task DeleteAsync(long id)
        {
            var result = await dataset.SingleOrDefaultAsync(p => p.Id.Equals(id));
            if (result != null)
            {
                try
                {
                    dataset.Remove(result);
                    await _context.SaveChangesAsync();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public bool Exists(Expression<Func<T, bool>> filter = null)
        {
            return dataset.Any(filter);
        }
    }
}
