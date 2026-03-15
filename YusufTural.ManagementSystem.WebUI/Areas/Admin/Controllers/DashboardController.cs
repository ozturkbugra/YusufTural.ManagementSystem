using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YusufTural.ManagementSystem.Business.Abstract;

namespace YusufTural.ManagementSystem.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class DashboardController : AdminBaseController
    {
        private readonly IVisitorService _visitorService;

        public DashboardController(IVisitorService visitorService)
        {
            _visitorService = visitorService;
        }

        public async Task<IActionResult> Index()
        {
            var visitors = await _visitorService.TGetListAsync();
            DateTime now = DateTime.Now;

            // Üst Kutucuklar için veriler
            ViewBag.DailyCount = visitors.Count(x => x.VisitedDate.Date == now.Date);
            ViewBag.MonthlyCount = visitors.Count(x => x.VisitedDate.Month == now.Month && x.VisitedDate.Year == now.Year);
            ViewBag.YearlyCount = visitors.Count(x => x.VisitedDate.Year == now.Year);

            // En çok ziyaret edilen 10 gün (Top 10 Days)
            var topDays = visitors
                .GroupBy(x => x.VisitedDate.Date)
                .Select(g => new { Day = g.Key.ToString("dd.MM.yyyy"), Count = g.Count() })
                .OrderByDescending(x => x.Count)
                .Take(10)
                .ToList();

            ViewBag.TopDaysLabels = topDays.Select(x => x.Day).ToList();
            ViewBag.TopDaysValues = topDays.Select(x => x.Count).ToList();

            // En çok ziyaret edilen 3 ay (Top 3 Months)
            var topMonths = visitors
                .GroupBy(x => new { x.VisitedDate.Year, x.VisitedDate.Month })
                .Select(g => new {
                    Month = new DateTime(g.Key.Year, g.Key.Month, 1).ToString("MMMM yyyy"),
                    Count = g.Count()
                })
                .OrderByDescending(x => x.Count)
                .Take(3)
                .ToList();

            ViewBag.TopMonthsLabels = topMonths.Select(x => x.Month).ToList();
            ViewBag.TopMonthsValues = topMonths.Select(x => x.Count).ToList();

            return View();
        }
    }
}