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
    }
}
