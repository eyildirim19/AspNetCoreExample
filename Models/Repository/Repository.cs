using AspNetCoreExample.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreExample.Models.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext _dbContext;
        private readonly DbSet<T> _dbSet;
        public Repository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        public int Add(T entity)
        {
            _dbSet.Add(entity);
            _dbContext.SaveChanges();
            return 1;
        }

        public int Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public T GetFindById(int id)
        {
            return _dbSet.Find(id);
        }

        public List<T> GetList()
        {
            return _dbSet.ToList();
            //return _dbContext.Set<T>().ToList();
        }

        public int Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
