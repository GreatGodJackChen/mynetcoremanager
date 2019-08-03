using CJ.Data.NetCoreModels;
using CJ.Models;
using CJ.Repositories.BaseRepositories;

namespace CJ.Application.CoreDbContext.CoreUserApp
{
    public class UserAppService : IUserAppService
    {
        private readonly IRepository<CoreUser> _repository;
        public UserAppService(IRepository<CoreUser> repository)
        {
            _repository = repository;
        }
        //public CoreUser GetUser(string loginName)
        //{
        //    var user = _repository.FirstOrDefault(a=>a.LoginName== loginName);
        //    //if (user != null)
        //    //{
        //    //    return new LoginInputModel() { };
        //    //}
        //    return null;
        //}
        public LoginInputModel GetUser(string loginName)
        {
            var user = _repository.FirstOrDefault(a => a.LoginName == loginName);
            if (user != null)
            {
                return new LoginInputModel() {Username=user.LoginName,Password=user.Password, DisplayName=user.DisplayName,Id=user.Id};
            }
            return null;
        }
    }
}
