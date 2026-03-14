using Microsoft.EntityFrameworkCore;
using YusufTural.ManagementSystem.DataAccess;
using YusufTural.ManagementSystem.Business;

var builder = WebApplication.CreateBuilder(args);

//Bağlantı cümlesini alıyoruz.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

//Interface ve Classların kaydı
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddBusinessServices();


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
