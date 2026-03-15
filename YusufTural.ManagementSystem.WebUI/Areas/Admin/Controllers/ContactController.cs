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
            var values = await _contactService.TGetListAsync();
            var data = values.FirstOrDefault();
            return View(data);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Contact model)
        {
            await _contactService.TAddAsync(model);
            await _contactService.TSaveAsync();
            CreateMessage("İletişim bilgileri başarıyla sisteme eklendi.", "success");
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var value = await _contactService.TGetByIdAsync(id);
            if (value == null) return NotFound();
            return View(value);
        }

        [HttpPost]
        public IActionResult Edit(Contact model)
        {
            var existingData = _contactService.TGetByIdAsync(model.Id).Result;
            if (existingData == null) return NotFound();

            // Alan eşleştirmeleri
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
    }
}
