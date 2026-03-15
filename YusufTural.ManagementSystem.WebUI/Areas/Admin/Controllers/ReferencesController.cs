using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YusufTural.ManagementSystem.WebUI.Helpers;
using YusufTural.ManagementSystem.Business.Abstract;
using YusufTural.ManagementSystem.Entities.Concrete;


namespace YusufTural.ManagementSystem.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class ReferencesController : AdminBaseController
    {
        private readonly IReferenceService _referenceService;

        public ReferencesController(IReferenceService referenceService)
        {
            _referenceService = referenceService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var values = await _referenceService.TGetListAsync();
            return View(values);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Reference model, IFormFile logoImage)
        {
            try
            {
                if (logoImage != null)
                    model.LogoUrl = await FileHelper.UploadFile(logoImage, "references");

                await _referenceService.TAddAsync(model);
                await _referenceService.TSaveAsync();
                CreateMessage("Referans kaydı başarıyla oluşturuldu.", "success");
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
            var value = await _referenceService.TGetByIdAsync(id);
            if (value == null) return NotFound();
            return View(value);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Reference model, IFormFile? logoImage)
        {
            try
            {
                var existingData = await _referenceService.TGetByIdAsync(model.Id);
                if (existingData == null) return NotFound();

                if (logoImage != null)
                {
                    FileHelper.DeleteFile(existingData.LogoUrl);
                    existingData.LogoUrl = await FileHelper.UploadFile(logoImage, "references");
                }

                _referenceService.TUpdate(existingData);
                _referenceService.TSave();
                CreateMessage("Referans bilgileri güncellendi.", "info");
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
            var value = await _referenceService.TGetByIdAsync(id);
            if (value == null) return NotFound();

            FileHelper.DeleteFile(value.LogoUrl);
            _referenceService.TDelete(value);
            _referenceService.TSave();

            CreateMessage("Referans kaydı silindi.", "warning");
            return RedirectToAction("Index");
        }
    }
}
