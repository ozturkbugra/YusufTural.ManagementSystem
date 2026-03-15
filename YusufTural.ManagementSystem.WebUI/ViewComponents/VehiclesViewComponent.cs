using Microsoft.AspNetCore.Mvc;
using YusufTural.ManagementSystem.Business.Abstract;

namespace YusufTural.ManagementSystem.WebUI.ViewComponents
{
    public class VehiclesViewComponent : ViewComponent
    {
        private readonly IVehicleService _s;

        public VehiclesViewComponent(IVehicleService s) => _s = s;

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _s.TGetListAsync();

            if (values == null || !values.Any())
            {
                return Content(string.Empty);
            }

            return View(values);
        }
    }
}