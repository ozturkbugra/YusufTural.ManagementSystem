using YusufTural.ManagementSystem.Business.Abstract;
using YusufTural.ManagementSystem.DataAccess.Abstract;
using YusufTural.ManagementSystem.Entities.Concrete;

namespace YusufTural.ManagementSystem.Business.Concrete
{
    public class ReferenceManager : GenericManager<Reference>, IReferenceService
    {
        public ReferenceManager(IGenericRepository<Reference> repository) : base(repository) { }
    }
}
