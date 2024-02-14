using FilRougeAPI.Repositories;
using Microsoft.EntityFrameworkCore;
namespace FilRougeAPI.Services
{
    public class Service<T> : IService<T> where T : class
    {
        private readonly IRepository<T> _repository;

        public Service(IRepository<T> repository)
        {
            _repository = repository;
        }
        public IQueryable<T> GetAll()
        {
            return _repository.GetAll();
        }

        public T? GetById(int id)
        {
            return _repository.GetById(id);
        }
        public T Add(T entity)
        {
            return _repository.Add(entity);
        }

        public bool Delete(int id)
        {
            var entity = _repository.GetById(id);
            if (entity == null)
            {
                return false;
            }
            _repository.Delete(entity);
            return true;
        }

        public T Update(T entity)
        {
            return (_repository.Update(entity));
        }

        public bool Exist(int id)
        {
            return  null != _repository.GetById(id);
        }
    }
}
