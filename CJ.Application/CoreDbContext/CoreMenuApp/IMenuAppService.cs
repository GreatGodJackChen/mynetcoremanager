using CJ.Data.NetCoreModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace CJ.Application.CoreDbContext.CoreMenuApp
{
    public interface IMenuAppService
    {
        List<CoreMenu> GetMenus();
    }
}
