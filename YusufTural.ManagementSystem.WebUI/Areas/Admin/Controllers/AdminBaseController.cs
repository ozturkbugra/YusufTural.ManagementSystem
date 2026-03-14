using Microsoft.AspNetCore.Mvc;

namespace YusufTural.ManagementSystem.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminBaseController : Controller
    {
        public void CreateMessage(string message, string type = "error")
        {
            TempData["AdminMessage"] = message;
            TempData["AdminMessageType"] = type;
        }
    }
}
