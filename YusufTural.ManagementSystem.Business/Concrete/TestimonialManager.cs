using YusufTural.ManagementSystem.Business.Abstract;
using YusufTural.ManagementSystem.DataAccess.Abstract;
using YusufTural.ManagementSystem.Entities.Concrete;

namespace YusufTural.ManagementSystem.Business.Concrete
{
    public class TestimonialManager : GenericManager<Testimonial>, ITestimonialService
    {
        private readonly IGenericRepository<Testimonial> _repository;

        public TestimonialManager(IGenericRepository<Testimonial> repository) : base(repository)
        {
            _repository = repository;
        }

        public override async Task TAddAsync(Testimonial entity)
        {
            var allTestimonials = await _repository.GetAllAsync();

            // Kural: Aynı isimde başka bir yorum var mı? (Büyük/küçük harf duyarsız)
            if (allTestimonials.Any(x => x.FullName.Trim().ToLower() == entity.FullName.Trim().ToLower()))
            {
                throw new Exception("Bu isimle zaten bir müşteri yorumu mevcut!");
            }

            await base.TAddAsync(entity);
        }

        public override void TUpdate(Testimonial entity)
        {
            var allTestimonials = _repository.GetAllAsync().GetAwaiter().GetResult();

            // Kural: Kendi ID'si hariç aynı isimle başka kayıt var mı?
            if (allTestimonials.Any(x => x.Id != entity.Id && x.FullName.Trim().ToLower() == entity.FullName.Trim().ToLower()))
            {
                throw new Exception("Güncellemek istediğiniz isim başka bir yorumda zaten kullanılıyor!");
            }

            base.TUpdate(entity);
        }
    }
}
