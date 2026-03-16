using Microsoft.AspNetCore.Mvc;
using YusufTural.ManagementSystem.Business.Abstract;

namespace YusufTural.ManagementSystem.WebUI.ViewComponents
{
    public class FooterScriptsViewComponent : ViewComponent
    {
        private readonly ISiteScriptService _s;
        public FooterScriptsViewComponent(ISiteScriptService s) => _s = s;

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var scripts = (await _s.TGetListAsync()).Where(x => x.IsActive && x.Position == 3).ToList();
            if (!scripts.Any()) return Content(string.Empty);
            return View(scripts);
        }
    }
}
