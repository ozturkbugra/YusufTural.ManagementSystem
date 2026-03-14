using YusufTural.ManagementSystem.Business.Abstract;
using YusufTural.ManagementSystem.DataAccess.Abstract;
using YusufTural.ManagementSystem.Entities.Concrete;

namespace YusufTural.ManagementSystem.Business.Concrete
{
    public class AppUserManager : GenericManager<AppUser>, IAppUserService
    {
        private readonly IGenericRepository<AppUser> _repository;

        public AppUserManager(IGenericRepository<AppUser> repository) : base(repository)
        {
            _repository = repository;
        }

        public override async Task TAddAsync(AppUser entity)
        {
            var allUsers = await _repository.GetAllAsync();
            var isUsernameExists = allUsers.Any(x => x.Username.ToLower() == entity.Username.ToLower());

            if (isUsernameExists)
            {
                throw new Exception("Bu kullanıcı adı zaten alınmış. Lütfen farklı bir kullanıcı adı seçin.");
            }

            await base.TAddAsync(entity);
        }

        public override void TDelete(AppUser entity)
        {
            var userCount = _repository.GetAllAsync().GetAwaiter().GetResult().Count;

            if (userCount <= 1)
            {
                throw new Exception("Sistemde en az bir kullanıcı bulunmalıdır. Son kullanıcıyı silemezsiniz!");
            }

            base.TDelete(entity);
        }

        public override void TUpdate(AppUser entity)
        {
            // 1. Veritabanındaki tüm kullanıcıları çekiyoruz
            var allUsers = _repository.GetAllAsync().GetAwaiter().GetResult();

            // 2. Kural: "Kendi ID'si hariç", aynı kullanıcı adına sahip başka biri var mı?
            var isUsernameExists = allUsers.Any(x =>
                x.Username.ToLower() == entity.Username.ToLower() &&
                x.Id != entity.Id);

            if (isUsernameExists)
            {
                throw new Exception("Bu kullanıcı adı başka bir kullanıcı tarafından kullanılıyor!");
            }

            base.TUpdate(entity);
        }
    }
}
