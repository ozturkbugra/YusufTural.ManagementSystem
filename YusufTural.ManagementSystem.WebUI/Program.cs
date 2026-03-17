using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using YusufTural.ManagementSystem.Business;
using YusufTural.ManagementSystem.Business.Abstract;
using YusufTural.ManagementSystem.Business.Concrete;
using YusufTural.ManagementSystem.DataAccess;
using YusufTural.ManagementSystem.DataAccess.Concrete;
using YusufTural.ManagementSystem.WebUI.Middlewares;

var builder = WebApplication.CreateBuilder(args);

//Bağlantı cümlesini alıyoruz.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

//Interface ve Classların kaydı
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddBusinessServices();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Admin/Login/Index";
        options.LogoutPath = "/Admin/Login/Logout";
        options.AccessDeniedPath = "/Admin/Login/Index";
        options.Cookie.Name = "YusufTuralManagementCookie";
        options.ExpireTimeSpan = TimeSpan.FromDays(1);
    });

builder.Services.Configure<Microsoft.AspNetCore.Http.Features.FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 104857600; // 100 MB limit
});

builder.Services.AddScoped<IVisitorService, VisitorManager>();

builder.Services.AddDataProtection()
    .PersistKeysToFileSystem(new DirectoryInfo(Path.Combine(builder.Environment.ContentRootPath, "keys")))
    .SetApplicationName("YusufTuralManagementSystem");


builder.Services.AddControllersWithViews();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    await DatabaseSeeder.SeedAsync(context);
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseMiddleware<VisitorTrackingMiddleware>();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Anasayfa}/{action=Index}/{id?}");

app.Run();
