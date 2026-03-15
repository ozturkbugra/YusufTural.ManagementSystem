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
            var values = await _aboutUsService.TGetListAsync();
            var data = values.FirstOrDefault();
            return View(data);
        }

        [HttpGet]
        public IActionResult Create()
        { 
            return View(); 
        }

        [HttpPost]
        public async Task<IActionResult> Create(AboutUs model, IFormFile aboutImage)
        {
            if (aboutImage != null)
                model.ImageUrl = await FileHelper.UploadFile(aboutImage, "about");

            await _aboutUsService.TAddAsync(model);
            await _aboutUsService.TSaveAsync();

            CreateMessage("Hakkımızda bilgisi başarıyla eklendi aga!", "success");
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var value = await _aboutUsService.TGetByIdAsync(id);

            if (value == null)
            {
                return NotFound();
            }

            return View(value);
        }

        [HttpPost]
        public IActionResult Edit(AboutUs model, IFormFile? aboutImage)
        {
            var existingData = _aboutUsService.TGetByIdAsync(model.Id).Result;
            if (existingData == null) return NotFound();

            existingData.Description = model.Description;

            if (aboutImage != null)
            {
                FileHelper.DeleteFile(existingData.ImageUrl);
                existingData.ImageUrl = FileHelper.UploadFile(aboutImage, "about").Result;
            }

            _aboutUsService.TUpdate(existingData);
            _aboutUsService.TSave();

            CreateMessage("Hakkımızda sayfası güncellendi aga!", "info");
            return RedirectToAction("Index");
        }
    }
}
