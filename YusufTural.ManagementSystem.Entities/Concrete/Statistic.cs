using YusufTural.ManagementSystem.Entities.Common;

namespace YusufTural.ManagementSystem.Entities.Concrete
{
    public sealed class Statistic : BaseEntity
    {
        public int HappyCustomerCount { get; set; }
        public int TransportedWaterAmount { get; set; }
        public int CompletedJobCount { get; set; }
        public int YearsOfExperience { get; set; }
    }
}
