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
        PaginatedList<CorePermission> GetList(int currentPage, int pageSize, Expression<Func<CorePermission, bool>> where);
    }
}
