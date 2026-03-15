using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YusufTural.ManagementSystem.Business.Abstract;
using YusufTural.ManagementSystem.Entities.Concrete;

namespace YusufTural.ManagementSystem.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class FaqController : AdminBaseController
    {
        private readonly IFaqService _faqService;

        public FaqController(IFaqService faqService)
        {
            _faqService = faqService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var values = await _faqService.TGetListAsync();
            return View(values);
        }

        [HttpGet]
        public IActionResult Create() {
            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Faq model)
        {
            try
            {
                await _faqService.TAddAsync(model);
                await _faqService.TSaveAsync();

                CreateMessage("Yeni soru-cevap kaydı başarıyla oluşturuldu.", "success");
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
            var value = await _faqService.TGetByIdAsync(id);
            if (value == null) return NotFound();
            return View(value);
        }

        [HttpPost]
        public IActionResult Edit(Faq model)
        {
            try
            {
                var existingData = _faqService.TGetByIdAsync(model.Id).Result;
                if (existingData == null) return NotFound();

                existingData.Question = model.Question;
                existingData.Answer = model.Answer;

                _faqService.TUpdate(existingData);
                _faqService.TSave();

                CreateMessage("Soru-cevap bilgileri başarıyla güncellendi.", "info");
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
            var value = await _faqService.TGetByIdAsync(id);
            if (value == null) return NotFound();

            _faqService.TDelete(value);
            _faqService.TSave();

            CreateMessage("Kayıt sistemden başarıyla silindi.", "warning");
            return RedirectToAction("Index");
        }
    }
}
