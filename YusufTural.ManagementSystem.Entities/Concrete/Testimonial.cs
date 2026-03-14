using YusufTural.ManagementSystem.Entities.Common;

namespace YusufTural.ManagementSystem.Entities.Concrete
{
    public sealed class Testimonial : BaseEntity
    {
        public string FullName { get; set; }
        public string Title { get; set; } // Ünvan (Örn: Şantiye Şefi)
        public string Comment { get; set; }
    }
}
