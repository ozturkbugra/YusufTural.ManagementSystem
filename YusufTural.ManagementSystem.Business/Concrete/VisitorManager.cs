using YusufTural.ManagementSystem.Business.Abstract;
using YusufTural.ManagementSystem.DataAccess.Abstract;
using YusufTural.ManagementSystem.Entities.Concrete;

namespace YusufTural.ManagementSystem.Business.Concrete
{
    public class VisitorManager : GenericManager<Visitor>, IVisitorService
    {
        private readonly IGenericRepository<Visitor> _repository;

        public VisitorManager(IGenericRepository<Visitor> repository) : base(repository)
        {
            _repository = repository;
        }

        public override async Task TAddAsync(Visitor entity)
        {
            entity.VisitedDate = DateTime.Now;

            if (string.IsNullOrEmpty(entity.VisitorKey))
            {
                entity.VisitorKey = Guid.NewGuid().ToString();
            }

            await base.TAddAsync(entity);
        }

        public override void TUpdate(Visitor entity)
        {
            throw new Exception("Gerçekleşmiş bir ziyaret kaydı üzerinde değişiklik yapılamaz!");
        }
    }
}
