using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using YusufTural.ManagementSystem.DataAccess.Abstract;
using YusufTural.ManagementSystem.DataAccess.Concrete;

namespace YusufTural.ManagementSystem.DataAccess
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped(typeof(IGenericRepository<>), typeof(EfRepository<>));
        }
    }
}
