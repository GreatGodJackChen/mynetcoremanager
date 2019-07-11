using CJ.Data.NetCoreModels;
using CJ.Repositories.BaseRepositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace CJ.Application.CoreDbContext.CoreUserApp
{
    public class UserAppService : IUserAppService
    {
        private readonly IRepository<CoreUser> _repository;
        public UserAppService(IRepository<CoreUser> repository)
        {
            _repository = repository;
        }
        public CoreUser GetUser(CoreUser requser)
        {
            //密码加密处理
            var user = _repository.FirstOrDefault(a=>a.LoginName==requser.LoginName && a.Password== requser.Password);
            return user;
        }
    }
}
