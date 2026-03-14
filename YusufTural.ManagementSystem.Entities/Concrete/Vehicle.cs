using YusufTural.ManagementSystem.Entities.Common;

namespace YusufTural.ManagementSystem.Entities.Concrete
{
    public sealed class Vehicle : BaseEntity
    {
        public string ImageUrl { get; set; }
        public string Description { get; set; }
    }
}
