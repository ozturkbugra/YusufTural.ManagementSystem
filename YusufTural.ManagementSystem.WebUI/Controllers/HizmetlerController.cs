using Microsoft.AspNetCore.Mvc;
using YusufTural.ManagementSystem.Business.Abstract;

namespace YusufTural.ManagementSystem.WebUI.Controllers
{
    public class HizmetlerController : Controller
    {
        private readonly IServiceService _serviceService;

        public HizmetlerController(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }

        [HttpGet("hizmet/{url}")]
        public async Task<IActionResult> Detay(string url)
        {
            if (string.IsNullOrEmpty(url)) return NotFound();

            var services = await _serviceService.TGetListAsync();
            var service = services.FirstOrDefault(x => x.Url == url);

            if (service == null) return NotFound();

            return View(service);
        }
    }
}
