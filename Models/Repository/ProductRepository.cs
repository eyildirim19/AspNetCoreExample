using AspNetCoreExample.Models.Entities;

namespace AspNetCoreExample.Models.Repository
{
    public class ProductRepository : Repository<Products>
    {
        public ProductRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}