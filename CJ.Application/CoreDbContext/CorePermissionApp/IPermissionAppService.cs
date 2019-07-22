using CJ.Data.NetCoreModels;
using CJ.Repositories.BaseRepositories;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CJ.Application.CoreDbContext.CorePermissionApp
{
    public interface IPermissionAppService
    {
        string AddPermission(Dictionary<string, string> dic);
        PaginatedList<CorePermission> GetList(int currentPage, int pageSize, Expression<Func<CorePermission, bool>> predicate, Expression<Func<CorePermission, DateTime?>> orderby);
        PaginatedList<CorePermission> Update(int currentPage, int pageSize, Expression<Func<CorePermission, bool>> predicate, CorePermission permission, Expression<Func<CorePermission, DateTime?>> orderby);
        PaginatedList<CorePermission> Delete(int currentPage, int pageSize,  string[] strId, Expression<Func<CorePermission, DateTime?>> orderby);
    }
}
