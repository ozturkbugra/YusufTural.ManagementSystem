using Microsoft.AspNetCore.Mvc;
using YusufTural.ManagementSystem.Business.Abstract;

namespace YusufTural.ManagementSystem.WebUI.ViewComponents
{
    public class SiteHeaderViewComponent : ViewComponent
    {
        private readonly ISiteInformationService _siteInformationService;
        private readonly IServiceService _serviceService;

        public SiteHeaderViewComponent(ISiteInformationService siteInformationService, IServiceService serviceService)
        {
            _siteInformationService = siteInformationService;
            _serviceService = serviceService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var siteInfo = (await _siteInformationService.TGetListAsync()).FirstOrDefault();
            var services = await _serviceService.TGetListAsync();

            ViewBag.Services = services;
            return View(siteInfo);
        }
    }
}
