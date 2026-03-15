using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YusufTural.ManagementSystem.Business.Abstract;
using YusufTural.ManagementSystem.Entities.Concrete;

namespace YusufTural.ManagementSystem.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class StatisticsController : AdminBaseController
    {
        private readonly IStatisticService _statisticService;

        public StatisticsController(IStatisticService statisticService)
        {
            _statisticService = statisticService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var values = await _statisticService.TGetListAsync();
            var data = values.FirstOrDefault();
            return View(data);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Statistic model)
        {
            try
            {
                await _statisticService.TAddAsync(model);
                await _statisticService.TSaveAsync();
                CreateMessage("İstatistik bilgileri başarıyla sisteme kaydedildi.", "success");
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
            var value = await _statisticService.TGetByIdAsync(id);
            if (value == null) return NotFound();
            return View(value);
        }

        [HttpPost]
        public IActionResult Edit(Statistic model)
        {
            try
            {
                var existingData = _statisticService.TGetByIdAsync(model.Id).Result;
                if (existingData == null) return NotFound();

                existingData.HappyCustomerCount = model.HappyCustomerCount;
                existingData.TransportedWaterAmount = model.TransportedWaterAmount;
                existingData.CompletedJobCount = model.CompletedJobCount;
                existingData.YearsOfExperience = model.YearsOfExperience;

                _statisticService.TUpdate(existingData);
                _statisticService.TSave();

                CreateMessage("İstatistikler başarıyla güncellendi.", "info");
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                CreateMessage(ex.Message, "danger");
                return View(model);
            }
        }
    }
}
