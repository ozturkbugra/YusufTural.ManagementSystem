using YusufTural.ManagementSystem.Business.Abstract;
using YusufTural.ManagementSystem.DataAccess.Abstract;
using YusufTural.ManagementSystem.Entities.Concrete;

namespace YusufTural.ManagementSystem.Business.Concrete
{
    public class SiteScriptManager : GenericManager<SiteScript>, ISiteScriptService
    {
        private readonly IGenericRepository<SiteScript> _repository;

        public SiteScriptManager(IGenericRepository<SiteScript> repository) : base(repository)
        {
            _repository = repository;
        }

        public override async Task TAddAsync(SiteScript entity)
        {
            var allScripts = await _repository.GetAllAsync();

            // Title veya Content'ten herhangi biri çakışıyorsa durdur
            if (allScripts.Any(x => x.Title.Trim().ToLower() == entity.Title.Trim().ToLower()))
                throw new Exception("Bu başlıkla bir script zaten kayıtlı!");

            if (allScripts.Any(x => x.Content.Trim() == entity.Content.Trim()))
                throw new Exception("Bu script içeriği sistemde zaten mevcut!");

            await base.TAddAsync(entity);
        }

        public override void TUpdate(SiteScript entity)
        {
            var allScripts = _repository.GetAllAsync().GetAwaiter().GetResult();

            // Kendi ID'si hariç kontrol
            if (allScripts.Any(x => x.Id != entity.Id && x.Title.Trim().ToLower() == entity.Title.Trim().ToLower()))
                throw new Exception("Başka bir kayıt zaten bu başlığı kullanıyor!");

            if (allScripts.Any(x => x.Id != entity.Id && x.Content.Trim() == entity.Content.Trim()))
                throw new Exception("Bu script içeriği başka bir kayıtta zaten tanımlanmış!");

            base.TUpdate(entity);
        }

        public override void TDelete(SiteScript entity)
        {
            throw new Exception("Script içeriği silinemez. Güncelleme işlemini yapın ya da pasife çekin.");
        }
    }
}
