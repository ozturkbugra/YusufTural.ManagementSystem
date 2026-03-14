using Microsoft.AspNetCore.Mvc;

namespace YusufTural.ManagementSystem.WebUI.Areas.Admin.ViewComponents
{
    public class AdminSidebarViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
