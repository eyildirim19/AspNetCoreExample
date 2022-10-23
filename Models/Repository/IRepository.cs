using AspNetCoreExample.Models.Entities;
using System.Linq.Expressions;

namespace AspNetCoreExample.Models.Repository
{
    public interface IRepository<T>
    {
        List<T> GetList();
        List<T> GetList(Expression<Func<T, bool>> expression);
        T GetFindById(int id);
        int Add(T entity);
        int Update(T entity);
        int Delete(T entity);
    }
}
