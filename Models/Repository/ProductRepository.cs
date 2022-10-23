using AspNetCoreExample.Models.Entities;
using System.Linq.Expressions;

namespace AspNetCoreExample.Models.Repository
{
    public class ProductRepository : Repository<Products>
    {
        AppDbContext _dbContext;
        public ProductRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}