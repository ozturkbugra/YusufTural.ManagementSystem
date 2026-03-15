using YusufTural.ManagementSystem.Business.Abstract;
using YusufTural.ManagementSystem.DataAccess.Abstract;
using YusufTural.ManagementSystem.Entities.Concrete;

namespace YusufTural.ManagementSystem.Business.Concrete
{
    public class VehicleManager : GenericManager<Vehicle>, IVehicleService
    {
        public VehicleManager(IGenericRepository<Vehicle> repository) : base(repository) { }
    }
}
