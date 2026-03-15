using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YusufTural.ManagementSystem.Business.Abstract;
using YusufTural.ManagementSystem.Entities.Concrete;

namespace YusufTural.ManagementSystem.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class ContactController : AdminBaseController
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var values = await _contactService.TGetListAsync();
                var data = values.FirstOrDefault();
                return View(data);
            }
            catch (Exception ex)
            {
                CreateMessage("Veriler listelenirken bir hata oluştu: " + ex.Message, "danger");
                return View();
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Contact model)
        {
            try
            {
                await _contactService.TAddAsync(model);
                await _contactService.TSaveAsync();
                CreateMessage("İletişim bilgileri başarıyla sisteme eklendi.", "success");
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                CreateMessage("Kayıt sırasında hata oluştu: " + ex.Message, "danger");
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var value = await _contactService.TGetByIdAsync(id);
            if (value == null) return NotFound();
            return View(value);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Contact model)
        {
            try
            {
                var existingData = await _contactService.TGetByIdAsync(model.Id);
                if (existingData == null) return NotFound();

                existingData.Address = model.Address;
                existingData.PhoneNumber = model.PhoneNumber;
                existingData.WhatsAppNumber = model.WhatsAppNumber;
                existingData.Email = model.Email;
                existingData.WorkingHours = model.WorkingHours;
                existingData.MapLink = model.MapLink;
                existingData.InstagramLink = model.InstagramLink;

                _contactService.TUpdate(existingData);
                _contactService.TSave();

                CreateMessage("İletişim bilgileri başarıyla güncellendi.", "info");
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
