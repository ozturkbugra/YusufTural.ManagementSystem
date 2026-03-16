using Microsoft.AspNetCore.Mvc;
using YusufTural.ManagementSystem.Business.Abstract;

namespace YusufTural.ManagementSystem.WebUI.ViewComponents
{
    public class SiteHeaderViewComponent : ViewComponent
    {
        private readonly ISiteInformationService _siteInformationService;
        private readonly IServiceService _serviceService;
        private readonly IAboutUsService _aboutService; // Eklendi
        private readonly IFaqService _faqService;     // Eklendi

        public SiteHeaderViewComponent(
            ISiteInformationService siteInformationService,
            IServiceService serviceService,
            IAboutUsService aboutService,
            IFaqService faqService)
        {
            _siteInformationService = siteInformationService;
            _serviceService = serviceService;
            _aboutService = aboutService;
            _faqService = faqService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var siteInfo = (await _siteInformationService.TGetListAsync()).FirstOrDefault();
            var services = await _serviceService.TGetListAsync();

            // Veri var mı kontrolü yapıyoruz aga
            ViewBag.HasAbout = (await _aboutService.TGetListAsync()).Any();
            ViewBag.HasFaq = (await _faqService.TGetListAsync()).Any();
            ViewBag.Services = services;

            return View(siteInfo);
        }
    }
}