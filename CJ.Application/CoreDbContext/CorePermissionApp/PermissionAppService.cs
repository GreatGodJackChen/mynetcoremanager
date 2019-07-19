using CJ.Data.NetCoreModels;
using CJ.Repositories.BaseRepositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace CJ.Application.CoreDbContext.CorePermissionApp
{
    public class PermissionAppService: IPermissionAppService
    {
        private readonly IRepository<CorePermission> _repository;
        public PermissionAppService(IRepository<CorePermission> repository)
        {
            _repository = repository;
        }
        public string AddPermission(CorePermission permission)
        {
            var result = _repository.InsertAndGetId(permission);
            return result;
        }
        public List<CorePermission> GetList()
        {
            var result = _repository.FindListPage(1, 20);
            return result;
        }
    }
}
