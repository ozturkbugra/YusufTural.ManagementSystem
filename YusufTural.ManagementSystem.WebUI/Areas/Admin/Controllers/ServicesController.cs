using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YusufTural.ManagementSystem.Business.Abstract;
using YusufTural.ManagementSystem.Entities.Concrete;
using YusufTural.ManagementSystem.WebUI.Helpers;

namespace YusufTural.ManagementSystem.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class ServicesController : AdminBaseController
    {
        private readonly IServiceService _serviceService;

        public ServicesController(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var values = await _serviceService.TGetListAsync();
                return View(values);
            }
            catch (Exception ex)
            {
                CreateMessage("Hizmetler listelenirken hata oluştu: " + ex.Message, "danger");
                return View();
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Service model, IFormFile serviceImage)
        {
            try
            {
                if (serviceImage != null)
                    model.ImageUrl = await FileHelper.UploadFile(serviceImage, "services");

                await _serviceService.TAddAsync(model);
                await _serviceService.TSaveAsync();

                CreateMessage("Yeni hizmet başarıyla eklendi.", "success");
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                CreateMessage("Kayıt sırasında hata oluştu: " + ex.Message, "danger");
                return View(model);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var value = await _serviceService.TGetByIdAsync(id);
                if (value == null) return NotFound();

                if (!string.IsNullOrEmpty(value.ImageUrl))
                {
                    FileHelper.DeleteFile(value.ImageUrl);
                }

                _serviceService.TDelete(value);
                _serviceService.TSave();

                CreateMessage("Hizmet ve bağlı görseli başarıyla silindi.", "warning");
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                CreateMessage("Silme işlemi başarısız: " + ex.Message, "danger");
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var value = await _serviceService.TGetByIdAsync(id);
            if (value == null) return NotFound();
            return View(value);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Service model, IFormFile? serviceImage)
        {
            try
            {
                var existingData = await _serviceService.TGetByIdAsync(model.Id);
                if (existingData == null) return NotFound();

                existingData.Title = model.Title;
                existingData.ShortDescription = model.ShortDescription;
                existingData.LongDescription = model.LongDescription;
                existingData.SeoTitle = model.SeoTitle;
                existingData.SeoKeywords = model.SeoKeywords;
                existingData.SeoDescription = model.SeoDescription;

                if (serviceImage != null)
                {
                    if (!string.IsNullOrEmpty(existingData.ImageUrl))
                    {
                        FileHelper.DeleteFile(existingData.ImageUrl);
                    }
                    existingData.ImageUrl = await FileHelper.UploadFile(serviceImage, "services");
                }

                _serviceService.TUpdate(existingData);
                _serviceService.TSave();

                CreateMessage("Hizmet bilgileri başarıyla güncellendi.", "info");
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                CreateMessage("Güncelleme sırasında hata oluştu: " + ex.Message, "danger");
                return View(model);
            }
        }
    }
}
