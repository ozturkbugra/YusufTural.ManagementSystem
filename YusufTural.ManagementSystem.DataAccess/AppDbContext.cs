using Microsoft.EntityFrameworkCore;
using YusufTural.ManagementSystem.Entities.Concrete;

namespace YusufTural.ManagementSystem.DataAccess
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Site Genel Bilgileri
        public DbSet<SiteInformation> SiteInformations { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<AboutUs> AboutUs { get; set; }
        public DbSet<Statistic> Statistics { get; set; }

        // Hizmetler ve Araçlar
        public DbSet<Service> Services { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }

        // Sosyal Kanıtlar
        public DbSet<Reference> References { get; set; }
        public DbSet<Testimonial> Testimonials { get; set; }
        public DbSet<Faq> Faqs { get; set; }

        // Sistem ve Yönetim
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Visitor> Visitors { get; set; }
        public DbSet<SiteScript> SiteScripts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {           
            base.OnModelCreating(modelBuilder);
        }
    }
}
