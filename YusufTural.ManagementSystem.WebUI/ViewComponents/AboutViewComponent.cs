using Microsoft.AspNetCore.Mvc;
using YusufTural.ManagementSystem.Business.Abstract;

namespace YusufTural.ManagementSystem.WebUI.ViewComponents
{
    public class AboutViewComponent : ViewComponent
    {
        private readonly IAboutUsService _s;
        public AboutViewComponent(IAboutUsService s) => _s = s;
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var value = (await _s.TGetListAsync()).FirstOrDefault();

            if (value == null)
            {
                return Content(string.Empty);
            }

            return View(value);
        }
    }
}
