using profilefinder_back.Repositories.Interfaces;
using TechChallengeTwo.Data;
using TechChallengeTwo.Models.Entities.Blog;

namespace profilefinder_back.Repositories.Implementations
{
    public class BlogPostRepository : BaseRepository<BlogPost>, IBlogPostRepository
    {
        public BlogPostRepository(ApplicationDbContext context) : base(context) { }

    }
}
