using AspNetCoreExample.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreExample.Models.Repository
{
    // Crud (Create,Read,Update,Delete) operasyonları Repository sınıflarında tutulur



    public class CategoryRepository : Repository<Categories>
    {
        public CategoryRepository(AppDbContext dbContext) : base(dbContext)
        { }
    }


    //public class CategoryRepository
    //{
    //    AppDbContext _dbContext;
    //    public CategoryRepository(AppDbContext dbContext)
    //    {
    //        _dbContext = dbContext;
    //    }
    //    // Read
    //    public List<Categories> GetList()
    //    {
    //        return _dbContext.Categories.ToList();
    //    }
    //    public Categories GetFindById(int id)
    //    {
    //        return _dbContext.Categories.Find(id);
    //    }
    //    public int Add(Categories categories)
    //    {
    //        _dbContext.Categories.Add(categories);
    //        _dbContext.SaveChanges();
    //        return 1;
    //    }
    //    public int Update(Categories categories)
    //    {
    //        //var cat = _dbContext.Categories.Find(categories.CategoryId);
    //        //cat.CategoryName = categories.CategoryName;

    //        // Güncelleme 
    //        _dbContext.Entry(categories).State = EntityState.Modified;
    //        _dbContext.SaveChanges();

    //        return 1;
    //    }
    //    public int Delete(Categories categories)
    //    {
    //        //var cat = _dbContext.Categories.Find(categories.CategoryId);
    //        //cat.CategoryName = categories.CategoryName;

    //        // Güncelleme 
    //        _dbContext.Categories.Remove(categories);
    //        _dbContext.SaveChanges();

    //        return 1;
    //    }

    //}
}