using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YusufTural.ManagementSystem.Business.Abstract;
using YusufTural.ManagementSystem.Entities.Concrete;
using YusufTural.ManagementSystem.WebUI.Helpers;

namespace YusufTural.ManagementSystem.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class SiteInformationController : AdminBaseController
    {
        private readonly ISiteInformationService _siteInformationService;

        public SiteInformationController(ISiteInformationService siteInformationService)
        {
            _siteInformationService = siteInformationService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var values = await _siteInformationService.TGetListAsync();
            return View(values);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(SiteInformation model, IFormFile logoFile, IFormFile faviconFile, IFormFile bigFaviconFile, IFormFile videoFile)
        {
            // Ekleme aşamasında dosya kontrolleri
            if (logoFile != null) model.Logo = await FileHelper.UploadFile(logoFile, "images");
            if (faviconFile != null) model.Favicon = await FileHelper.UploadFile(faviconFile, "images");
            if (bigFaviconFile != null) model.BigFavicon = await FileHelper.UploadFile(bigFaviconFile, "images");
            if (videoFile != null) model.VideoUrl = await FileHelper.UploadFile(videoFile, "videos");

            await _siteInformationService.TAddAsync(model);
            await _siteInformationService.TSaveAsync();
            CreateMessage("Site bilgileri başarıyla eklendi aga!", "success");
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var value = await _siteInformationService.TGetByIdAsync(id);
            return View(value);
        }


        [HttpPost]
        public IActionResult Edit(SiteInformation model, IFormFile? logoFile, IFormFile? faviconFile, IFormFile? bigFaviconFile, IFormFile? videoFile)
        {
            var existingData = _siteInformationService.TGetByIdAsync(model.Id).Result;

            if (existingData == null) return NotFound();

            existingData.Name = model.Name;
            existingData.Slogan = model.Slogan;
            existingData.SloganDescription = model.SloganDescription;
            existingData.SeoTitle = model.SeoTitle;
            existingData.SeoKeyword = model.SeoKeyword;
            existingData.SeoDescription = model.SeoDescription;


            if (logoFile != null)
            {
                FileHelper.DeleteFile(existingData.Logo);
                existingData.Logo = FileHelper.UploadFile(logoFile, "images").Result;
            }

            if (faviconFile != null)
            {
                FileHelper.DeleteFile(existingData.Favicon);
                existingData.Favicon = FileHelper.UploadFile(faviconFile, "images").Result;
            }

            if (bigFaviconFile != null)
            {
                FileHelper.DeleteFile(existingData.BigFavicon);
                existingData.BigFavicon = FileHelper.UploadFile(bigFaviconFile, "images").Result;
            }

            if (videoFile != null)
            {
                FileHelper.DeleteFile(existingData.VideoUrl);
                existingData.VideoUrl = FileHelper.UploadFile(videoFile, "videos").Result;
            }

            _siteInformationService.TUpdate(existingData);
            _siteInformationService.TSave();

            CreateMessage("Site ayarları ve medya dosyaları jilet gibi güncellendi aga!", "info");
            return RedirectToAction("Index");
        }
    }
}