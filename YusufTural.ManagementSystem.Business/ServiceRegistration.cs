using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        }
    }
}
