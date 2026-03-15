using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YusufTural.ManagementSystem.Business.Abstract;
using YusufTural.ManagementSystem.Entities.Concrete;

namespace YusufTural.ManagementSystem.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class VisitorsController : AdminBaseController
    {
        private readonly IVisitorService _visitorService;

        public VisitorsController(IVisitorService visitorService)
        {
            _visitorService = visitorService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(DateTime? startDate, DateTime? endDate)
        {
            try
            {
                DateTime now = DateTime.Now;

                if (!startDate.HasValue)
                {
                    startDate = new DateTime(now.Year, now.Month, 1);
                }

                if (!endDate.HasValue)
                {
                    endDate = startDate.Value.AddMonths(1).AddDays(-1);
                }

                var values = await _visitorService.TGetListAsync();

                values = values.Where(x => x.VisitedDate >= startDate.Value &&
                                         x.VisitedDate <= endDate.Value.AddDays(1).AddTicks(-1)).ToList();

                var chartData = values
                    .GroupBy(x => x.VisitedDate.Date)
                    .OrderBy(x => x.Key)
                    .Select(g => new {
                        Date = g.Key.ToString("dd MMM"),
                        Count = g.Count()
                    }).ToList();

                ViewBag.ChartLabels = chartData.Select(x => x.Date).ToList();
                ViewBag.ChartValues = chartData.Select(x => x.Count).ToList();

                ViewBag.StartDate = startDate?.ToString("yyyy-MM-dd");
                ViewBag.EndDate = endDate?.ToString("yyyy-MM-dd");

                return View(values.OrderByDescending(x => x.VisitedDate).ToList());
            }
            catch (Exception ex)
            {
                CreateMessage("İstatistikler yüklenirken bir hata oluştu: " + ex.Message, "danger");
                return View(new List<Visitor>());
            }
        }
    }
}
