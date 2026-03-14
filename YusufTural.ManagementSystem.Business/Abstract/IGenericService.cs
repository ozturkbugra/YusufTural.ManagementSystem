namespace YusufTural.ManagementSystem.Business.Abstract
{
    public interface IGenericService<T> where T : class
    {
        Task<List<T>> TGetListAsync();
        Task<T> TGetByIdAsync(int id);
        Task TAddAsync(T entity);
        void TUpdate(T entity);
        void TDelete(T entity);
        Task TSaveAsync();
        void TSave();
    }
}
