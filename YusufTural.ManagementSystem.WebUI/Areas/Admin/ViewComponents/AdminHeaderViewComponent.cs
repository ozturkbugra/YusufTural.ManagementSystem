using Microsoft.AspNetCore.Mvc;

namespace YusufTural.ManagementSystem.WebUI.Areas.Admin.ViewComponents
{
    public class AdminHeaderViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
