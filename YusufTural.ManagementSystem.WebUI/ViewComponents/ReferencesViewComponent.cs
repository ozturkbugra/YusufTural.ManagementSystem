using Microsoft.AspNetCore.Mvc;
using YusufTural.ManagementSystem.Business.Abstract;

namespace YusufTural.ManagementSystem.WebUI.ViewComponents
{
    public class ReferencesViewComponent : ViewComponent
    {
        private readonly IReferenceService _s;
        public ReferencesViewComponent(IReferenceService s) => _s = s;
        public async Task<IViewComponentResult> InvokeAsync() => View(await _s.TGetListAsync());
    }
}
