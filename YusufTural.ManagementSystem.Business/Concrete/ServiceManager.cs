using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using YusufTural.ManagementSystem.Business.Abstract;
using YusufTural.ManagementSystem.DataAccess.Abstract;
using YusufTural.ManagementSystem.Entities.Concrete;

namespace YusufTural.ManagementSystem.Business.Concrete
{
    public class ServiceManager : GenericManager<Service>, IServiceService
    {
        private readonly IGenericRepository<Service> _repository;

        public ServiceManager(IGenericRepository<Service> repository) : base(repository)
        {
            _repository = repository;
        }

        public override async Task TAddAsync(Service entity)
        {
            var allServices = await _repository.GetAllAsync();

            // 1. URL boşsa başlıktan oluştur, doluysa temizle
            entity.Url = string.IsNullOrEmpty(entity.Url) ? CreateSlug(entity.Title) : CreateSlug(entity.Url);

            // 2. Uniqueness (Benzersizlik) Kontrolü
            if (allServices.Any(x => x.Title.ToLower() == entity.Title.ToLower()))
                throw new Exception("Bu hizmet başlığı zaten mevcut!");

            if (allServices.Any(x => x.Url == entity.Url))
                throw new Exception("Bu SEO URL (link) başka bir hizmet tarafından kullanılıyor!");

            await base.TAddAsync(entity);
        }

        public override void TUpdate(Service entity)
        {
            var allServices = _repository.GetAllAsync().GetAwaiter().GetResult();
            entity.Url = CreateSlug(entity.Url);

            // Kendi ID'si hariç çakışma var mı?
            if (allServices.Any(x => x.Id != entity.Id && x.Title.ToLower() == entity.Title.ToLower()))
                throw new Exception("Bu başlık başka bir hizmette zaten kullanılıyor!");

            if (allServices.Any(x => x.Id != entity.Id && x.Url == entity.Url))
                throw new Exception("Bu SEO URL başka bir hizmette zaten kullanılıyor!");

            base.TUpdate(entity);
        }

        // Yardımcı Metot: Türkçe Karakterleri Temizle ve URL Formatına Getir
        private string CreateSlug(string text)
        {
            if (string.IsNullOrEmpty(text)) return "";
            text = text.ToLower().Trim();
            text = text.Replace("ş", "s").Replace("ı", "i").Replace("ç", "c").Replace("ö", "o").Replace("ü", "u").Replace("ğ", "g");
            text = Regex.Replace(text, @"[^a-z0-9\s-]", ""); // Harf, sayı ve boşluk dışındakileri at
            text = Regex.Replace(text, @"\s+", " ").Trim(); // Birden fazla boşluğu teke indir
            text = text.Replace(" ", "-"); // Boşlukları tire yap
            return text;
        }
    }
}
