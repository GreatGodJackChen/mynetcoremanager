using CJ.Data.NetCoreModels;
using CJ.Entities.NetCore;
using CJ.Repositories.BaseRepositories;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CJ.Application.CoreDbContext.CorePermissionApp
{
    public interface IPermissionAppService
    {
        string AddPermission(Dictionary<string, string> dic);
        PaginatedList<PermissionEntity> GetList(int currentPage, int pageSize, Expression<Func<PermissionEntity, bool>> predicate);
        PaginatedList<PermissionEntity> Update(int currentPage, int pageSize, Expression<Func<PermissionEntity, bool>> predicate, CorePermission permission);
        PaginatedList<CorePermission> Delete(int currentPage, int pageSize,  string[] strId);
    }
}
