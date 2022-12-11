namespace webapi.DataLayer.Repositories.Base
{
    public interface IRepository<T> where T : class
    {
        List<T> GetAll();
        T GetById(int id);
        bool Insert(T entity);
        bool Update(T entity);
        bool Delete(T entity);
    }
}
