using YusufTural.ManagementSystem.Entities.Common;

namespace YusufTural.ManagementSystem.Entities.Concrete
{
    public sealed class AppUser : BaseEntity
    {
        public string Username { get; set; }
        public string Password { get; set; } 
    }
}
