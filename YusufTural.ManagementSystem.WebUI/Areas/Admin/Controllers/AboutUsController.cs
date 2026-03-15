using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YusufTural.ManagementSystem.Business.Abstract;
using YusufTural.ManagementSystem.Entities.Concrete;
using YusufTural.ManagementSystem.WebUI.Helpers;

namespace YusufTural.ManagementSystem.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class AboutUsController : AdminBaseController
    {
        private readonly IAboutUsService _aboutUsService;

        public AboutUsController(IAboutUsService aboutUsService)
        {
            _aboutUsService = aboutUsService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var values = await _aboutUsService.TGetListAsync();
                var data = values.FirstOrDefault();
                return View(data);
            }
            catch (Exception ex)
            {
                CreateMessage("Veriler yüklenirken bir hata oluştu: " + ex.Message, "danger");
                return View();
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AboutUs model, IFormFile aboutImage)
        {
            try
            {
                if (aboutImage != null)
                    model.ImageUrl = await FileHelper.UploadFile(aboutImage, "about");

                await _aboutUsService.TAddAsync(model);
                await _aboutUsService.TSaveAsync();

                CreateMessage("Hakkımızda bilgileri başarıyla eklendi.", "success");
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                CreateMessage("Hata oluştu: " + ex.Message, "danger");
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var value = await _aboutUsService.TGetByIdAsync(id);
            if (value == null) return NotFound();
            return View(value);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AboutUs model, IFormFile? aboutImage)
        {
            try
            {
                var existingData = await _aboutUsService.TGetByIdAsync(model.Id);
                if (existingData == null) return NotFound();

                existingData.Description = model.Description;

                if (aboutImage != null)
                {
                    if (!string.IsNullOrEmpty(existingData.ImageUrl))
                    {
                        FileHelper.DeleteFile(existingData.ImageUrl);
                    }
                    existingData.ImageUrl = await FileHelper.UploadFile(aboutImage, "about");
                }

                _aboutUsService.TUpdate(existingData);
                _aboutUsService.TSave();

                CreateMessage("Hakkımızda sayfası başarıyla güncellendi.", "info");
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                CreateMessage("Güncelleme başarısız: " + ex.Message, "danger");
                return View(model);
            }
        }
    }
}
