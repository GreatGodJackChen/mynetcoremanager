using CJ.Data.NetCoreModels;
using CJ.Repositories.BaseRepositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
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
        public string AddPermission(Dictionary<string, string> dic)
        {
            CorePermission permission = new CorePermission()
            {
                Name = dic["Name"],
                MenuId = dic["MenuId"],
                ActionCode = dic["ActionCode"],
            };
            var result = _repository.InsertAndGetId(permission);
            return result;
        }
        public PaginatedList<CorePermission> GetList(int currentPage,int pageSize, Expression<Func<CorePermission, bool>> where)
        {
            var result = _repository.FindListPage(currentPage, pageSize,where);
            return result;
        }
    }
}
