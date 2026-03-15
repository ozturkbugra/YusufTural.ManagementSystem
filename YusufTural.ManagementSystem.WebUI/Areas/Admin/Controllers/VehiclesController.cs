using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YusufTural.ManagementSystem.Business.Abstract;
using YusufTural.ManagementSystem.Entities.Concrete;
using YusufTural.ManagementSystem.WebUI.Helpers;

namespace YusufTural.ManagementSystem.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class VehiclesController : AdminBaseController
    {
        private readonly IVehicleService _vehicleService;

        public VehiclesController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var values = await _vehicleService.TGetListAsync();
            return View(values);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Vehicle model, IFormFile vehicleImage)
        {
            try
            {
                if (vehicleImage != null)
                    model.ImageUrl = await FileHelper.UploadFile(vehicleImage, "vehicles");
                
                if(model.Description == null)
                     model.Description = "YUSUF TURAL TANKER SU TAŞIMACILIK";

                await _vehicleService.TAddAsync(model);
                await _vehicleService.TSaveAsync();
                CreateMessage("Araç başarıyla filoya eklendi.", "success");
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                CreateMessage(ex.Message, "danger");
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var value = await _vehicleService.TGetByIdAsync(id);
            if (value == null) return NotFound();
            return View(value);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Vehicle model, IFormFile? vehicleImage)
        {
            try
            {
                var existingData = await _vehicleService.TGetByIdAsync(model.Id);
                if (existingData == null) return NotFound();

                if (vehicleImage != null)
                {
                    FileHelper.DeleteFile(existingData.ImageUrl);
                    existingData.ImageUrl = await FileHelper.UploadFile(vehicleImage, "vehicles");
                }

                existingData.Description = model.Description;

                _vehicleService.TUpdate(existingData);
                _vehicleService.TSave();
                CreateMessage("Araç bilgileri güncellendi.", "info");
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                CreateMessage(ex.Message, "danger");
                return View(model);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            var value = await _vehicleService.TGetByIdAsync(id);
            if (value == null) return NotFound();

            FileHelper.DeleteFile(value.ImageUrl);
            _vehicleService.TDelete(value);
            _vehicleService.TSave();

            CreateMessage("Araç filodan kaldırıldı.", "warning");
            return RedirectToAction("Index");
        }
    }
}
