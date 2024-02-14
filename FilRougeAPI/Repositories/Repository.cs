using FilRougeAPI.Configs;
using Microsoft.EntityFrameworkCore;

namespace FilRougeAPI.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly BibliothequeDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public Repository(BibliothequeDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public IQueryable<T> GetAll()
        {
            return _dbSet.AsQueryable();
        }

        public T? GetById(int id)
        {
            return _dbSet.Find(id);
        }
        public T Add(T entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
            _context.SaveChanges();
        }

        public T Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
            return entity;
        }

    }
}
