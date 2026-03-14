using YusufTural.ManagementSystem.Business.Abstract;
using YusufTural.ManagementSystem.DataAccess.Abstract;
using YusufTural.ManagementSystem.Entities.Concrete;

namespace YusufTural.ManagementSystem.Business.Concrete
{
    public class AboutUsManager : GenericManager<AboutUs>, IAboutUsService
    {
        private readonly IGenericRepository<AboutUs> _repository;

        public AboutUsManager(IGenericRepository<AboutUs> repository) : base(repository)
        {
            _repository = repository;
        }

        public override async Task TAddAsync(AboutUs entity)
        {
            var existingData = await _repository.GetAllAsync();

            if (existingData.Count > 0)
            {
                throw new Exception("Hakkımızda bölümü için zaten bir kayıt mevcut. Lütfen mevcut kaydı güncelleyin.");
            }

            await base.TAddAsync(entity);
        }

        public override void TDelete(AboutUs entity)
        {
            throw new Exception("Hakkımızda içeriği silinemez. Lütfen içeriği güncelleyin.");
        }
    }
}
