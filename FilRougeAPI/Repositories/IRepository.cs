namespace FilRougeAPI.Repositories
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        T? GetById(int id);
        T Add(T entity);
        T Update(T entity);
        void Delete(T entity);
    }
}
