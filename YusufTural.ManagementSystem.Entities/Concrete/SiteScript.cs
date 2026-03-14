using YusufTural.ManagementSystem.Entities.Common;

namespace YusufTural.ManagementSystem.Entities.Concrete
{
    public sealed class SiteScript : BaseEntity
    {
        public string Title { get; set; }
        public string Content { get; set; } // Ajansçının vereceği <script> etiketi
        public int Position { get; set; } // 1: Head, 2: Body Start, 3: Footer
        public bool IsActive { get; set; }
    }
}
