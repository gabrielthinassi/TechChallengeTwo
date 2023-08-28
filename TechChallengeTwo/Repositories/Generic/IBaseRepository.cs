using System.Linq.Expressions;
using TechChallengeTwo.Models.Entities;

namespace profilefinder_back.Repositories.Interfaces
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task<T> CreateAsync(T item);
        Task<T> FindByIdAsync(long id);
        Task<List<T>> FindAsync(Expression<Func<T, bool>> filter = null);
        Task<T> UpdateAsync(T item);
        Task DeleteAsync(long id);
        bool Exists(Expression<Func<T, bool>> filter = null);
    }
}
