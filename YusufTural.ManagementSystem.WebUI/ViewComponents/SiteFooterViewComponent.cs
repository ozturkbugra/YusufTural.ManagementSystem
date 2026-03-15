using Microsoft.AspNetCore.Mvc;
using YusufTural.ManagementSystem.Business.Abstract;

namespace YusufTural.ManagementSystem.WebUI.ViewComponents
{
    public class SiteFooterViewComponent : ViewComponent
    {
        private readonly ISiteInformationService _siteInformationService;
        private readonly IContactService _contactService;
        private readonly IServiceService _serviceService;

        public SiteFooterViewComponent(ISiteInformationService siteInformationService, IContactService contactService, IServiceService serviceService)
        {
            _siteInformationService = siteInformationService;
            _contactService = contactService;
            _serviceService = serviceService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var siteInfo = (await _siteInformationService.TGetListAsync()).FirstOrDefault();
            var contact = (await _contactService.TGetListAsync()).FirstOrDefault();
            var services = await _serviceService.TGetListAsync();

            ViewBag.Services = services;
            ViewBag.Contact = contact;

            return View(siteInfo);
        }
    }
}
