using YusufTural.ManagementSystem.Business.Abstract;
using YusufTural.ManagementSystem.DataAccess.Abstract;

namespace YusufTural.ManagementSystem.Business.Concrete
{
    public class GenericManager<T> : IGenericService<T> where T : class
    {
        private readonly IGenericRepository<T> _repository;

        public GenericManager(IGenericRepository<T> repository)
        {
            _repository = repository;
        }

        public virtual async Task TAddAsync(T entity) => await _repository.AddAsync(entity);

        public virtual void TDelete(T entity) => _repository.Delete(entity);

        public virtual void TUpdate(T entity) => _repository.Update(entity);

        public virtual async Task<T> TGetByIdAsync(int id) => await _repository.GetByIdAsync(id);

        public virtual async Task<List<T>> TGetListAsync() => await _repository.GetAllAsync();
    }
}
