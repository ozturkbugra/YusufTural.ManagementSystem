using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YusufTural.ManagementSystem.Business.Abstract;
using YusufTural.ManagementSystem.DataAccess.Tools;
using YusufTural.ManagementSystem.Entities.Concrete;

namespace YusufTural.ManagementSystem.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class AppUsersController : AdminBaseController
    {
        private readonly IAppUserService _appUserService;

        public AppUsersController(IAppUserService appUserService)
        {
            _appUserService = appUserService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var values = await _appUserService.TGetListAsync();
            return View(values);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AppUser model)
        {
            try
            {
                model.Password = Sha256Helper.ComputeSha256Hash(model.Password);
                await _appUserService.TAddAsync(model);
                await _appUserService.TSaveAsync();
                CreateMessage("Yeni yönetici hesabı güvenli bir şekilde oluşturuldu.", "success");
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
            var value = await _appUserService.TGetByIdAsync(id);
            if (value == null) return NotFound();
            return View(value);
        }

        [HttpPost]
        public IActionResult Edit(AppUser model)
        {
            try
            {
                var existingData = _appUserService.TGetByIdAsync(model.Id).Result;
                if (existingData == null) return NotFound();

                existingData.Username = model.Username;

                // KRİTİK NOKTA: Formdan gelen düz metin şifreyi hash'leyerek kaydediyoruz
                if (!string.IsNullOrEmpty(model.Password))
                {
                    existingData.Password = Sha256Helper.ComputeSha256Hash(model.Password);
                }

                _appUserService.TUpdate(existingData);
                _appUserService.TSave();

                CreateMessage("Yönetici bilgileri ve şifresi başarıyla güncellendi.", "info");
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                CreateMessage("Güncelleme sırasında bir hata oluştu: " + ex.Message, "danger");
                return View(model);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var value = await _appUserService.TGetByIdAsync(id);
                if (value == null)
                {
                    CreateMessage("Silinmek istenen yönetici hesabı bulunamadı.", "danger");
                    return RedirectToAction("Index");
                }

                _appUserService.TDelete(value);
                _appUserService.TSave();

                CreateMessage("Yönetici hesabı sistemden başarıyla kaldırıldı.", "warning");
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                CreateMessage("Hesap silinirken bir hata oluştu: " + ex.Message, "danger");
                return RedirectToAction("Index");
            }
        }
    }
}
