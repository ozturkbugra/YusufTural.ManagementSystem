using YusufTural.ManagementSystem.Business.Abstract;
using YusufTural.ManagementSystem.DataAccess.Abstract;
using YusufTural.ManagementSystem.Entities.Concrete;

namespace YusufTural.ManagementSystem.Business.Concrete
{
    public class StatisticManager : GenericManager<Statistic>, IStatisticService
    {
        private readonly IGenericRepository<Statistic> _repository;

        public StatisticManager(IGenericRepository<Statistic> repository) : base(repository)
        {
            _repository = repository;
        }

        public override async Task TAddAsync(Statistic entity)
        {
            var count = (await _repository.GetAllAsync()).Count;
            if (count > 0)
            {
                throw new Exception("İstatistik bilgileri zaten mevcut. İkinci bir kayıt ekleyemezsiniz!");
            }
            await base.TAddAsync(entity);
        }

        public override void TDelete(Statistic entity)
        {
            throw new Exception("İstatistik verileri silinemez, sadece güncellenebilir!");
        }
    }
}
