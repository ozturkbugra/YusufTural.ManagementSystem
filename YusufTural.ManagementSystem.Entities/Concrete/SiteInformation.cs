using YusufTural.ManagementSystem.Entities.Common;

namespace YusufTural.ManagementSystem.Entities.Concrete
{
    public sealed class SiteInformation : BaseEntity
    {
        public string Logo { get; set; }
        public string Name { get; set; }
        public string Slogan { get; set; }
        public string SloganDescription { get; set; } // Sitenin kısa açıklaması
        public string SeoDescription { get; set; } // SEO için
        public string SeoKeyword { get; set; } // SEO için
        public string SeoTitle { get; set; } // SEO için
        public string Favicon { get; set; }
        public string BigFavicon { get; set; }
    }
}
