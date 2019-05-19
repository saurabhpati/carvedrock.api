using CarvedRock.Repositories.Models;
using CarvedRockApi.Repositories.Models;
using Microsoft.EntityFrameworkCore;

namespace CarvedRock.Api.Data
{
    public class CarvedRockDbContext: DbContext
    {
        public CarvedRockDbContext(DbContextOptions<CarvedRockDbContext> options): base(options)
        {
            
        }
        public DbSet<Product> Products { get; set; }

        public DbSet<ProductReview> ProductReviews { get; set; }
    }
}
