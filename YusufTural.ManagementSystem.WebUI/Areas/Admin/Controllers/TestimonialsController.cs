using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YusufTural.ManagementSystem.Business.Abstract;
using YusufTural.ManagementSystem.Entities.Concrete;

namespace YusufTural.ManagementSystem.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class TestimonialsController : AdminBaseController
    {
        private readonly ITestimonialService _testimonialService;

        public TestimonialsController(ITestimonialService testimonialService)
        {
            _testimonialService = testimonialService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var values = await _testimonialService.TGetListAsync();
            return View(values);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Testimonial model)
        {
            try
            {
                await _testimonialService.TAddAsync(model);
                await _testimonialService.TSaveAsync();
                CreateMessage("Müşteri yorumu başarıyla sisteme eklendi.", "success");
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
            var value = await _testimonialService.TGetByIdAsync(id);
            if (value == null) return NotFound();
            return View(value);
        }

        [HttpPost]
        public IActionResult Edit(Testimonial model)
        {
            try
            {
                var existingData = _testimonialService.TGetByIdAsync(model.Id).Result;
                if (existingData == null) return NotFound();

                existingData.FullName = model.FullName;
                existingData.Title = model.Title;
                existingData.Comment = model.Comment;

                _testimonialService.TUpdate(existingData);
                _testimonialService.TSave();

                CreateMessage("Yorum bilgileri başarıyla güncellendi.", "info");
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
            var value = await _testimonialService.TGetByIdAsync(id);
            if (value == null) return NotFound();

            _testimonialService.TDelete(value);
            _testimonialService.TSave();

            CreateMessage("Müşteri yorumu sistemden silindi.", "warning");
            return RedirectToAction("Index");
        }
    }
}
