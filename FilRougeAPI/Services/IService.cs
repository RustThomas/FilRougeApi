namespace FilRougeAPI.Services
{
    public interface IService<T> where T : class
    {
        IQueryable<T> GetAll();
        T? GetById(int id);
        T Add(T entity);
        T Update(T entity);
        bool Delete(int id);

        bool Exist(int id);
    }
}
