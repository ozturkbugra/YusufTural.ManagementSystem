using Microsoft.AspNetCore.Mvc;

namespace YusufTural.ManagementSystem.WebUI.Areas.Admin
{
    [Area("Admin")]
    public class IndexController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
        }
    }
}
