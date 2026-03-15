using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YusufTural.ManagementSystem.Business.Abstract;
using YusufTural.ManagementSystem.Entities.Concrete;

namespace YusufTural.ManagementSystem.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class SiteScriptsController : AdminBaseController
    {
        private readonly ISiteScriptService _scriptService;

        public SiteScriptsController(ISiteScriptService scriptService)
        {
            _scriptService = scriptService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var values = await _scriptService.TGetListAsync();
            return View(values);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(SiteScript model)
        {
            try
            {
                await _scriptService.TAddAsync(model);
                await _scriptService.TSaveAsync();
                CreateMessage("Yeni script kaydı başarıyla oluşturuldu.", "success");
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
            var value = await _scriptService.TGetByIdAsync(id);
            if (value == null) return NotFound();
            return View(value);
        }

        [HttpPost]
        public IActionResult Edit(SiteScript model)
        {
            try
            {
                var existingData = _scriptService.TGetByIdAsync(model.Id).Result;
                if (existingData == null) return NotFound();

                existingData.Title = model.Title;
                existingData.Content = model.Content;
                existingData.Position = model.Position;
                existingData.IsActive = model.IsActive;

                _scriptService.TUpdate(existingData);
                _scriptService.TSave();

                CreateMessage("Script bilgileri başarıyla güncellendi.", "info");
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                CreateMessage(ex.Message, "danger");
                return View(model);
            }
        }

        public async Task<IActionResult> ChangeStatus(int id)
        {
            var value = await _scriptService.TGetByIdAsync(id);
            if (value == null) return NotFound();

            value.IsActive = !value.IsActive;
            _scriptService.TUpdate(value);
            _scriptService.TSave();

            string statusText = value.IsActive ? "aktif hale getirildi" : "pasife çekildi";
            CreateMessage($"Script başarıyla {statusText}.", "info");

            return RedirectToAction("Index");
        }
    }
}
