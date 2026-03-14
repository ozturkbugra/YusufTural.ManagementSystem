using Microsoft.EntityFrameworkCore;
using YusufTural.ManagementSystem.DataAccess.Tools;
using YusufTural.ManagementSystem.Entities.Concrete;

namespace YusufTural.ManagementSystem.DataAccess.Concrete
{
    public static class DatabaseSeeder
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            await context.Database.MigrateAsync();

            if (!await context.AppUsers.AnyAsync(x => x.Username == "admin"))
            {
                var adminUser = new AppUser
                {
                    Username = "admin",
                    Password = Sha256Helper.ComputeSha256Hash("123456"),
                };

                await context.AppUsers.AddAsync(adminUser);
                await context.SaveChangesAsync();
            }
        }
    }
}
