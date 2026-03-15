using Microsoft.AspNetCore.Mvc;
using YusufTural.ManagementSystem.Business.Abstract;

namespace YusufTural.ManagementSystem.WebUI.ViewComponents
{
    public class TestimonialsViewComponent : ViewComponent
    {
        private readonly ITestimonialService _testimonialService;
        private readonly ISiteInformationService _siteInfoService;

        public TestimonialsViewComponent(ITestimonialService testimonialService, ISiteInformationService siteInfoService)
        {
            _testimonialService = testimonialService;
            _siteInfoService = siteInfoService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var testimonials = await _testimonialService.TGetListAsync();
            var siteInfo = (await _siteInfoService.TGetListAsync()).FirstOrDefault();

            ViewBag.TestimonialBg = siteInfo?.BigImageUrl ?? "/site/assets/img/arkaplan.png";

            return View(testimonials);
        }
    }
}