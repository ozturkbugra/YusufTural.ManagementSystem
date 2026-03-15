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

            // 1. Bot Filtreleme (İstatistikleri kirletmeyelim aga)
            string[] bots = { "bot", "crawler", "spider", "slurp", "googlebot", "bingbot", "yandexbot", "baiduspider", "facebookexternalhit" };
            bool isBot = string.IsNullOrEmpty(userAgent) || bots.Any(bot => userAgent.Contains(bot));

            // 2. Takip Kısıtlamaları (Admin panelini ve statik dosyaları loglamıyoruz)
            bool isStaticFile = path.Contains(".js") || path.Contains(".css") || path.Contains(".jpg") ||
                               path.Contains(".png") || path.Contains(".webp") || path.Contains(".ico");

            if (!isBot && !isStaticFile && !path.StartsWith("/admin"))
            {
                // 3. VisitorKey (Cookie) Mantığı - Her tarayıcıya özel Guid
                if (!context.Request.Cookies.TryGetValue("V_Key", out string visitorKey))
                {
                    visitorKey = Guid.NewGuid().ToString();
                    context.Response.Cookies.Append("V_Key", visitorKey, new CookieOptions
                    {
                        Expires = DateTime.Now.AddYears(1),
                        HttpOnly = true, // Güvenlik için JS erişemez
                        SameSite = SameSiteMode.Strict
                    });
                }

                // 4. Kayıt Atma
                var visitor = new Visitor
                {
                    VisitorKey = visitorKey,
                    IpAddress = context.Connection.RemoteIpAddress?.ToString() ?? "127.0.0.1",
                    DeviceInfo = userAgent,
                    VisitedDate = DateTime.Now
                };

                // Asenkron olarak ekliyoruz, sayfayı bekletmez
                await visitorService.TAddAsync(visitor);
            }

            await _next(context);
        }
    }
}