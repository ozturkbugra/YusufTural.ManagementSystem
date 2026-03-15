using YusufTural.ManagementSystem.Business.Abstract;
using YusufTural.ManagementSystem.Entities.Concrete;

namespace YusufTural.ManagementSystem.WebUI.Middlewares
{
    public class VisitorTrackingMiddleware
    {
        private readonly RequestDelegate _next;

        public VisitorTrackingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IVisitorService visitorService)
        {
            var userAgent = context.Request.Headers["User-Agent"].ToString().ToLower();
            var path = context.Request.Path.Value?.ToLower() ?? "";

            // 1. Bot Filtreleme
            string[] bots = { "bot", "crawler", "spider", "slurp", "googlebot", "bingbot", "yandexbot", "baiduspider", "facebookexternalhit" };
            bool isBot = string.IsNullOrEmpty(userAgent) || bots.Any(bot => userAgent.Contains(bot));

            // 2. Takip Kısıtlamaları (Statik dosyalar ve Admin panelini geçiyoruz)
            bool isStaticFile = path.Contains(".js") || path.Contains(".css") || path.Contains(".jpg") ||
                               path.Contains(".png") || path.Contains(".webp") || path.Contains(".ico");

            if (!isBot && !isStaticFile && !path.StartsWith("/admin"))
            {
                // 3. VisitorKey (Cookie) Kontrolü
                if (!context.Request.Cookies.TryGetValue("V_Key", out string visitorKey))
                {
                    visitorKey = Guid.NewGuid().ToString();
                    context.Response.Cookies.Append("V_Key", visitorKey, new CookieOptions
                    {
                        Expires = DateTime.Now.AddYears(1),
                        HttpOnly = true,
                        SameSite = SameSiteMode.Strict
                    });
                }

                // --- YENİ MANTIK: GÜNLÜK TEK KAYIT KONTROLÜ ---
                // Veri tabanında bu anahtarla (VisitorKey) BUGÜN bir kayıt atılmış mı bakıyoruz.
                var allVisitors = await visitorService.TGetListAsync();
                var alreadyRecordedToday = allVisitors.Any(x =>
                    x.VisitorKey == visitorKey &&
                    x.VisitedDate.Date == DateTime.Today);

                if (!alreadyRecordedToday)
                {
                    // 4. Kayıt Atma (Sadece bugün ilk defa geliyorsa)
                    var visitor = new Visitor
                    {
                        VisitorKey = visitorKey,
                        IpAddress = context.Connection.RemoteIpAddress?.ToString() ?? "127.0.0.1",
                        DeviceInfo = userAgent,
                        VisitedDate = DateTime.Now
                    };

                    await visitorService.TAddAsync(visitor);
                }
            }

            await _next(context);
        }
    }
}