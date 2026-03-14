using YusufTural.ManagementSystem.Business.Abstract;
using YusufTural.ManagementSystem.DataAccess.Abstract;
using YusufTural.ManagementSystem.Entities.Concrete;

namespace YusufTural.ManagementSystem.Business.Concrete
{
    public class SiteInformationManager : GenericManager<SiteInformation>, ISiteInformationService
    {
        private readonly IGenericRepository<SiteInformation> _repository;

        public SiteInformationManager(IGenericRepository<SiteInformation> repository) : base(repository)
        {
            _repository = repository;
        }

        public override async Task TAddAsync(SiteInformation entity)
        {
            var existingData = await _repository.GetAllAsync();
            if (existingData.Any())
            {
                throw new Exception("Sistemde zaten bir site bilgisi mevcut. İkinci bir kayıt ekleyemezsiniz, lütfen mevcut olanı güncelleyin.");
            }
            await base.TAddAsync(entity);
        }
    }
}
