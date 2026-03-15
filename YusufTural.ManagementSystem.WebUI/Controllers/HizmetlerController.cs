using Microsoft.AspNetCore.Mvc;
using YusufTural.ManagementSystem.Business.Abstract;

namespace YusufTural.ManagementSystem.WebUI.Controllers
{
    public class HizmetlerController : Controller
    {
        private readonly IServiceService _serviceService;
        private readonly IContactService _contactService;
        private readonly ISiteInformationService _siteInfoService;

        public HizmetlerController(IServiceService serviceService, IContactService contactService, ISiteInformationService siteInfoService)
        {
            _serviceService = serviceService;
            _contactService = contactService;
            _siteInfoService = siteInfoService;
        }

        [HttpGet("hizmet/{url}")]
        public async Task<IActionResult> Detay(string url)
        {
            if (string.IsNullOrEmpty(url)) return NotFound();

            var allServices = await _serviceService.TGetListAsync();
            var service = allServices.FirstOrDefault(x => x.Url == url);

            if (service == null) return NotFound();

            var siteInfo = (await _siteInfoService.TGetListAsync()).FirstOrDefault();
            ViewBag.PageTitleBg = siteInfo?.BigImageUrl ?? "/site/assets/img/page-title-bg.webp";

            ViewBag.AllServices = allServices;
            ViewBag.Contact = (await _contactService.TGetListAsync()).FirstOrDefault();

            ViewData["Title"] = service.SeoTitle;
            ViewData["SeoDescription"] = service.SeoDescription;
            ViewData["SeoKeyword"] = service.SeoKeywords;
            ViewData["BodyClass"] = "service-details-page";
            ViewData["SiteName"] = siteInfo?.Name;

            return View(service);
        }
    }
}