using Microsoft.Extensions.DependencyInjection;
using YusufTural.ManagementSystem.Business.Abstract;
using YusufTural.ManagementSystem.Business.Concrete;

namespace YusufTural.ManagementSystem.Business
{
    public static class ServiceRegistration
    {
        public static void AddBusinessServices(this IServiceCollection services)
        {
            // Generic Servis Kaydı
            services.AddScoped(typeof(IGenericService<>), typeof(GenericManager<>));

            // Özel Servis Kayıtları
            services.AddScoped<ISiteInformationService, SiteInformationManager>();
            services.AddScoped<IAboutUsService, AboutUsManager>();
            services.AddScoped<IAppUserService, AppUserManager>();
            services.AddScoped<IContactService, ContactManager>();
        }
    }
}
