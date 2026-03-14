using Microsoft.EntityFrameworkCore;
using YusufTural.ManagementSystem.DataAccess.Abstract;

namespace YusufTural.ManagementSystem.DataAccess.Concrete
{
    public class EfRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly AppDbContext _context;
        public EfRepository(AppDbContext context) => _context = context;

        public async Task<List<T>> GetAllAsync() => await _context.Set<T>().ToListAsync();
        public async Task<T> GetByIdAsync(int id) => await _context.Set<T>().FindAsync(id);
        public async Task AddAsync(T entity) => await _context.Set<T>().AddAsync(entity);
        public void Update(T entity) => _context.Set<T>().Update(entity);
        public void Delete(T entity) => _context.Set<T>().Remove(entity);
        public async Task SaveAsync() => await _context.SaveChangesAsync();
        public async Task Save() => _context.SaveChanges();
    }
}
