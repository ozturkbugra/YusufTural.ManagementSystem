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
            try
            {
                var values = await _siteInformationService.TGetListAsync();
                return View(values);
            }
            catch (Exception ex)
            {
                CreateMessage("Veriler yüklenirken hata oluştu: " + ex.Message, "danger");
                return View();
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(SiteInformation model, IFormFile logoFile, IFormFile faviconFile, IFormFile bigFaviconFile, IFormFile videoFile, IFormFile bigImageFile)
        {
            try
            {
                if (logoFile != null) model.Logo = await FileHelper.UploadFile(logoFile, "images");
                if (faviconFile != null) model.Favicon = await FileHelper.UploadFile(faviconFile, "images");
                if (bigFaviconFile != null) model.BigFavicon = await FileHelper.UploadFile(bigFaviconFile, "images");
                if (videoFile != null) model.VideoUrl = await FileHelper.UploadFile(videoFile, "videos");
                if (bigImageFile != null) model.BigImageUrl = await FileHelper.UploadFile(bigImageFile, "images");

                await _siteInformationService.TAddAsync(model);
                await _siteInformationService.TSaveAsync();

                CreateMessage("Site bilgileri başarıyla eklendi.", "success");
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                CreateMessage("Kayıt sırasında bir hata oluştu: " + ex.Message, "danger");
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var value = await _siteInformationService.TGetByIdAsync(id);
            if (value == null) return NotFound();
            return View(value);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(SiteInformation model, IFormFile? logoFile, IFormFile? faviconFile, IFormFile? bigFaviconFile, IFormFile? videoFile, IFormFile? bigImageFile, bool deleteLogo = false, bool deleteVideo = false)
        {
            try
            {
                var existingData = await _siteInformationService.TGetByIdAsync(model.Id);
                if (existingData == null) return NotFound();

                // Metin alanlarını güncelliyoruz
                existingData.Name = model.Name;
                existingData.Slogan = model.Slogan;
                existingData.SloganDescription = model.SloganDescription;
                existingData.SeoTitle = model.SeoTitle;
                existingData.SeoKeyword = model.SeoKeyword;
                existingData.SeoDescription = model.SeoDescription;

                // --- Logo Yönetimi ---
                if (logoFile != null)
                {
                    if (!string.IsNullOrEmpty(existingData.Logo)) FileHelper.DeleteFile(existingData.Logo);
                    existingData.Logo = await FileHelper.UploadFile(logoFile, "images");
                }
                else if (deleteLogo)
                {
                    if (!string.IsNullOrEmpty(existingData.Logo)) FileHelper.DeleteFile(existingData.Logo);
                    existingData.Logo = null;
                }

                // --- Video Yönetimi ---
                if (videoFile != null)
                {
                    if (!string.IsNullOrEmpty(existingData.VideoUrl)) FileHelper.DeleteFile(existingData.VideoUrl);
                    existingData.VideoUrl = await FileHelper.UploadFile(videoFile, "videos");
                }
                else if (deleteVideo)
                {
                    if (!string.IsNullOrEmpty(existingData.VideoUrl)) FileHelper.DeleteFile(existingData.VideoUrl);
                    existingData.VideoUrl = null;
                }

                // --- Diğer Dosyalar (Favicon ve Arka Plan) ---
                if (faviconFile != null)
                {
                    if (!string.IsNullOrEmpty(existingData.Favicon)) FileHelper.DeleteFile(existingData.Favicon);
                    existingData.Favicon = await FileHelper.UploadFile(faviconFile, "images");
                }
                if (bigFaviconFile != null)
                {
                    if (!string.IsNullOrEmpty(existingData.BigFavicon)) FileHelper.DeleteFile(existingData.BigFavicon);
                    existingData.BigFavicon = await FileHelper.UploadFile(bigFaviconFile, "images");
                }
                if (bigImageFile != null)
                {
                    if (!string.IsNullOrEmpty(existingData.BigImageUrl)) FileHelper.DeleteFile(existingData.BigImageUrl);
                    existingData.BigImageUrl = await FileHelper.UploadFile(bigImageFile, "images");
                }

                _siteInformationService.TUpdate(existingData);
                _siteInformationService.TSave();

                CreateMessage("Site ayarları ve medya dosyaları başarıyla güncellendi.", "info");
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