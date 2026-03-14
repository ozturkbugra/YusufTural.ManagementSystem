using YusufTural.ManagementSystem.Business.Abstract;
using YusufTural.ManagementSystem.DataAccess.Abstract;
using YusufTural.ManagementSystem.Entities.Concrete;

namespace YusufTural.ManagementSystem.Business.Concrete
{
    public class FaqManager : GenericManager<Faq>, IFaqService
    {
        private readonly IGenericRepository<Faq> _repository;

        public FaqManager(IGenericRepository<Faq> repository) : base(repository)
        {
            _repository = repository;
        }

        public override async Task TAddAsync(Faq entity)
        {
            var allFaqs = await _repository.GetAllAsync();

            bool isDuplicate = allFaqs.Any(x =>
                x.Question.Trim().ToLower() == entity.Question.Trim().ToLower() &&
                x.Answer.Trim().ToLower() == entity.Answer.Trim().ToLower());

            if (isDuplicate)
            {
                throw new Exception("Bu soru ve cevap zaten sistemde mevcut!");
            }

            await base.TAddAsync(entity);
        }

        public override void TUpdate(Faq entity)
        {
            var allFaqs = _repository.GetAllAsync().GetAwaiter().GetResult();

            // Kural: Kendi ID'si hariç aynı soru-cevap ikilisi var mı?
            bool isDuplicate = allFaqs.Any(x =>
                x.Id != entity.Id &&
                x.Question.Trim().ToLower() == entity.Question.Trim().ToLower() &&
                x.Answer.Trim().ToLower() == entity.Answer.Trim().ToLower());

            if (isDuplicate)
            {
                throw new Exception("Güncellemek istediğiniz soru ve cevap kombinasyonu başka bir kayıtta zaten var!");
            }

            base.TUpdate(entity);
        }
    }
}
