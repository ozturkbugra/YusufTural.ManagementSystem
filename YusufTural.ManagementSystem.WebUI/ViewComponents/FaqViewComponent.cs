using Microsoft.AspNetCore.Mvc;
using YusufTural.ManagementSystem.Business.Abstract;

namespace YusufTural.ManagementSystem.WebUI.ViewComponents
{
    public class FaqViewComponent : ViewComponent
    {
        private readonly IFaqService _s;
        public FaqViewComponent(IFaqService s) => _s = s;
        public async Task<IViewComponentResult> InvokeAsync() => View(await _s.TGetListAsync());
    }
}
