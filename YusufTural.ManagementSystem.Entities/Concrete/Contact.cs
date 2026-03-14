using YusufTural.ManagementSystem.Entities.Common;

namespace YusufTural.ManagementSystem.Entities.Concrete
{
    public sealed class Contact : BaseEntity
    {
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string WhatsAppNumber { get; set; }
        public string Email { get; set; }
        public string WorkingHours { get; set; }
        public string MapLink { get; set; }
        public string InstagramLink { get; set; }
    }
}
