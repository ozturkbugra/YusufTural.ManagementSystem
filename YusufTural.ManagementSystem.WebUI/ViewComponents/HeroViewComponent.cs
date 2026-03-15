using Microsoft.AspNetCore.Mvc;
using YusufTural.ManagementSystem.Business.Abstract;

namespace YusufTural.ManagementSystem.WebUI.ViewComponents
{
    public class HeroViewComponent : ViewComponent
    {
        private readonly ISiteInformationService _siteInfoService;
        private readonly IContactService _contactService;

        public HeroViewComponent(ISiteInformationService siteInfoService, IContactService contactService)
        {
            _siteInfoService = siteInfoService;
            _contactService = contactService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var siteInfo = (await _siteInfoService.TGetListAsync()).FirstOrDefault();
            var contact = (await _contactService.TGetListAsync()).FirstOrDefault();

            ViewBag.WpNumber = contact?.WhatsAppNumber?.Replace(" ", "");

            return View(siteInfo);
        }
    }
}