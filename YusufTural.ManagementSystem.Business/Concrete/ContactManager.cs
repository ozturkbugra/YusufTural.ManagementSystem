using YusufTural.ManagementSystem.Business.Abstract;
using YusufTural.ManagementSystem.DataAccess.Abstract;
using YusufTural.ManagementSystem.Entities.Concrete;

namespace YusufTural.ManagementSystem.Business.Concrete
{
    public class ContactManager : GenericManager<Contact>, IContactService
    {
        private readonly IGenericRepository<Contact> _repository;

        public ContactManager(IGenericRepository<Contact> repository) : base(repository)
        {
            _repository = repository;
        }

        public override async Task TAddAsync(Contact entity)
        {
            var count = (await _repository.GetAllAsync()).Count;
            if (count > 0)
            {
                throw new Exception("İletişim bilgileri zaten mevcut. İkinci bir kayıt ekleyemezsiniz, lütfen mevcut olanı güncelleyin.");
            }
            await base.TAddAsync(entity);
        }

        public override void TDelete(Contact entity)
        {
            throw new Exception("İletişim bilgileri silinemez. Lütfen bilgileri güncelleyiniz.");
        }
    }
}
