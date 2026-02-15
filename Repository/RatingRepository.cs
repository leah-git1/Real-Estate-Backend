using Entities;
using Repository;
using System.Threading.Tasks;

namespace Repositories
{
    public class RatingRepository : IRatingRepository
    {
        private readonly ShopContext _apiDbContext;

        public RatingRepository(ShopContext apiDbContext)
        {
            _apiDbContext = apiDbContext;
        }

        public async Task<Rating> AddRating(Rating newRating)
        {
            await _apiDbContext.Ratings.AddAsync(newRating);
            await _apiDbContext.SaveChangesAsync();
            return newRating;
        }
    }
}
