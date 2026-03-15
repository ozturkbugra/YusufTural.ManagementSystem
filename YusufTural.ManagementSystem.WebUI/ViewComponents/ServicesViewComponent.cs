using Microsoft.AspNetCore.Mvc;
using YusufTural.ManagementSystem.Business.Abstract;

namespace YusufTural.ManagementSystem.WebUI.ViewComponents
{
    public class ServicesViewComponent : ViewComponent
    {
        private readonly IServiceService _service;
        public ServicesViewComponent(IServiceService service) => _service = service;
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var data = await _service.TGetListAsync();
            return View(data);
        }
    }
}
