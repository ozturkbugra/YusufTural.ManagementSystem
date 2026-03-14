using YusufTural.ManagementSystem.Entities.Common;

namespace YusufTural.ManagementSystem.Entities.Concrete
{
    public sealed class Faq : BaseEntity
    {
        public string Question { get; set; }
        public string Answer { get; set; }
    }
}
