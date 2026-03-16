using Microsoft.AspNetCore.Mvc;
using YusufTural.ManagementSystem.Business.Abstract;

namespace YusufTural.ManagementSystem.WebUI.ViewComponents
{
    public class HeadScriptsViewComponent : ViewComponent
    {
        private readonly ISiteScriptService _s;
        public HeadScriptsViewComponent(ISiteScriptService s) => _s = s;

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var scripts = (await _s.TGetListAsync()).Where(x => x.IsActive && x.Position == 1).ToList();
            if (!scripts.Any()) return Content(string.Empty);
            return View(scripts);
        }
    }
}
