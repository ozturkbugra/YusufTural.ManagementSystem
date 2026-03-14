using YusufTural.ManagementSystem.Entities.Common;

namespace YusufTural.ManagementSystem.Entities.Concrete
{
    public sealed class Statistic : BaseEntity
    {
        public string HappyCustomerCount { get; set; }
        public string TransportedWaterAmount { get; set; }
        public string CompletedJobCount { get; set; }
        public string YearsOfExperience { get; set; }
    }
}
