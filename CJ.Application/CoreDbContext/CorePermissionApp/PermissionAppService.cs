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
        public PaginatedList<CorePermission> GetList(int currentPage,int pageSize, Expression<Func<CorePermission, bool>> predicate, Expression<Func<CorePermission, DateTime?>> orderby)
        {
            var result = _repository.FindListPage(currentPage, pageSize, predicate, orderby,true);
            return result;
        }
        public PaginatedList<CorePermission> Update(int currentPage, int pageSize, Expression<Func<CorePermission, bool>> where, CorePermission permission, Expression<Func<CorePermission, DateTime?>> orderby)
        {
            _repository.UpdateColumn(permission, null,new string[] { "Name","MenuId", "ActionCode" });
            var result = _repository.FindListPage(currentPage, pageSize, orderby, true);
            return result;
        }
        public PaginatedList<CorePermission> Delete(int currentPage, int pageSize, string[] strId, Expression<Func<CorePermission, DateTime?>> orderby)
        {
            foreach (var id in strId)
            {
                _repository.Delete(id);
            }
            var result = _repository.FindListPage(currentPage, pageSize, orderby, true);
            return result;
        }
    }
}
