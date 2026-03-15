using Microsoft.AspNetCore.Mvc;
using YusufTural.ManagementSystem.Business.Abstract;

namespace YusufTural.ManagementSystem.WebUI.Controllers
{
    public class AnasayfaController : Controller
    {
        private readonly ISiteInformationService _siteInformationService;

        public AnasayfaController(ISiteInformationService siteInformationService)
        {
            _siteInformationService = siteInformationService;
        }

        public async Task<IActionResult> Index()
        {
            var siteInfo = (await _siteInformationService.TGetListAsync()).FirstOrDefault();

            ViewData["Title"] = siteInfo?.SeoTitle ?? "Anasayfa";
            ViewData["SiteName"] = siteInfo?.Name;
            ViewData["SeoDescription"] = siteInfo?.SeoDescription;
            ViewData["SeoKeyword"] = siteInfo?.SeoKeyword;
            ViewData["Favicon"] = siteInfo?.Favicon;
            ViewData["BigFavicon"] = siteInfo?.BigFavicon;
            ViewData["BodyClass"] = "index-page";

            return View();
        }
    }
}

