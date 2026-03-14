using Microsoft.EntityFrameworkCore;
using YusufTural.ManagementSystem.Business.Abstract;
using YusufTural.ManagementSystem.Business.Concrete;
using YusufTural.ManagementSystem.DataAccess;
using YusufTural.ManagementSystem.DataAccess.Abstract;
using YusufTural.ManagementSystem.DataAccess.Concrete;

var builder = WebApplication.CreateBuilder(args);

//Bağlantı cümlesini alıyoruz.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

//Interface ve Classların kaydı
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(EfRepository<>));
builder.Services.AddScoped(typeof(IGenericService<>), typeof(GenericManager<>));


builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
