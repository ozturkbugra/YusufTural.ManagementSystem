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
        private readonly IServiceService _serviceService; // Business katmanındaki servisin

        public ServicesController(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }

        public async Task<IActionResult> Index()
        {
            var values = await _serviceService.TGetListAsync();
            return View(values);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Service model, IFormFile serviceImage)
        {
            if (serviceImage != null)
                model.ImageUrl = await FileHelper.UploadFile(serviceImage, "services");

            await _serviceService.TAddAsync(model);
            await _serviceService.TSaveAsync();

            CreateMessage("Yeni hizmet başarıyla eklendi aga!", "success");
            return RedirectToAction("Index");
        }

        // 2. Silme İşlemi (İstediğin Kritik Kısım)
        public async Task<IActionResult> Delete(int id)
        {
            var value = await _serviceService.TGetByIdAsync(id);
            if (value == null) return NotFound();

            // Önce sunucudaki resmi temizliyoruz
            FileHelper.DeleteFile(value.ImageUrl);

            // Sonra veritabanından kaydı uçuruyoruz
            _serviceService.TDelete(value);
            _serviceService.TSave();

            CreateMessage("Hizmet ve bağlı görseli başarıyla silindi.", "warning");
            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var value = await _serviceService.TGetByIdAsync(id);
            if (value == null) return NotFound();

            return View(value);
        }

        // 4. Düzenleme İşlemi - POST
        [HttpPost]
        public IActionResult Edit(Service model, IFormFile? serviceImage)
        {
            // Veritabanındaki mevcut veriyi senkron olarak çekiyoruz
            var existingData = _serviceService.TGetByIdAsync(model.Id).Result;

            if (existingData == null) return NotFound();

            // Metin alanlarını güncelliyoruz
            existingData.Title = model.Title;
            existingData.ShortDescription = model.ShortDescription;
            existingData.LongDescription = model.LongDescription;
            existingData.SeoTitle = model.SeoTitle;
            existingData.SeoKeywords = model.SeoKeywords;
            existingData.SeoDescription = model.SeoDescription;

            // Eğer yeni bir resim seçildiyse; eskisini silip yenisini yüklüyoruz
            if (serviceImage != null)
            {
                // Eski dosyayı sunucudan temizle
                FileHelper.DeleteFile(existingData.ImageUrl);

                // Yeni dosyayı yükle ve yolunu güncelle
                existingData.ImageUrl = FileHelper.UploadFile(serviceImage, "services").Result;
            }

            // Değişiklikleri mühürle
            _serviceService.TUpdate(existingData);
            _serviceService.TSave();

            CreateMessage("Hizmet bilgileri başarıyla güncellendi aga!", "info");
            return RedirectToAction("Index");
        }
    }
}
