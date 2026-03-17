using Microsoft.AspNetCore.Mvc;

namespace YusufTural.ManagementSystem.WebUI.Controllers
{
    public class ErrorPageController : Controller
    {
        public IActionResult Error404()
        {
            ViewData["BodyClass"] = "service-details-page";
            Response.StatusCode = 404;
            return View();
        }
    }
}
