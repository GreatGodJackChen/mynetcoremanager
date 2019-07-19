using CJ.Data.NetCoreModels;
using System.Collections.Generic;

namespace CJ.Application.CoreDbContext.CorePermissionApp
{
    public interface IPermissionAppService
    {
        string AddPermission(CorePermission permission);
        List<CorePermission> GetList();
    }
}
