using Microsoft.AspNetCore.Mvc;
using YusufTural.ManagementSystem.Business.Abstract;

namespace YusufTural.ManagementSystem.WebUI.ViewComponents
{
    public class FaqViewComponent : ViewComponent
    {
        private readonly IFaqService _s;

        public FaqViewComponent(IFaqService s) => _s = s;

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _s.TGetListAsync();

            if (values == null || !values.Any())
            {
                return Content(string.Empty);
            }

            return View(values);
        }
    }
}