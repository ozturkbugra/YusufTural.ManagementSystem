using YusufTural.ManagementSystem.Entities.Common;

namespace YusufTural.ManagementSystem.Entities.Concrete
{
    public sealed class Visitor : BaseEntity
    {
        public string VisitorKey { get; set; } // Guid tutacağımız alan
        public string IpAddress { get; set; }
        public string DeviceInfo { get; set; } // User-Agent (Tarayıcı vs.)
        public DateTime VisitedDate { get; set; }
    }
}
