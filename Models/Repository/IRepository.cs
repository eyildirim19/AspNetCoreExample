using AspNetCoreExample.Models.Entities;

namespace AspNetCoreExample.Models.Repository
{
    public interface IRepository<T>
    {
        List<T> GetList();

        T GetFindById(int id);

        int Add(T entity);

        int Update(T entity);

        int Delete(T entity);
    }
}
