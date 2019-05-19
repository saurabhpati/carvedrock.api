using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarvedRock.Api.Data;
using CarvedRockApi.Repositories.Models;

namespace CarvedRockApi.Repositories
{
    public class ProductReviewRepository : Repository<ProductReview, int>, IProductReviewRepository
    {
        private CarvedRockDbContext _context;

        public ProductReviewRepository(CarvedRockDbContext context) : base(context)
        {
            _context = context;
        }

        public Task<ILookup<int, ProductReview>> GetReviewsByProductId(IEnumerable<int> productIds)
        {
            return Task.FromResult(_context.ProductReviews
                .Where(r => productIds.Any(pId => pId == r.ProductId))
                .ToLookup(x => x.ProductId));
        }
    }
}