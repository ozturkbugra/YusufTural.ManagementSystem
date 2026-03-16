using Microsoft.AspNetCore.Mvc;
using YusufTural.ManagementSystem.Business.Abstract;

namespace YusufTural.ManagementSystem.WebUI.ViewComponents
{
    public class BodyStartScriptsViewComponent : ViewComponent
    {
        private readonly ISiteScriptService _s;
        public BodyStartScriptsViewComponent(ISiteScriptService s) => _s = s;

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var scripts = (await _s.TGetListAsync()).Where(x => x.IsActive && x.Position == 2).ToList();
            if (!scripts.Any()) return Content(string.Empty);
            return View(scripts);
        }
    }
}
