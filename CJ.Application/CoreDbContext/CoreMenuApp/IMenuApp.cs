using CJ.Entities.MenuEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CJ.Application.CoreDbContext.CoreMenuApp
{
    public interface IMenuApp
    {
        List<MenuEntity> GetMenus();
    }
}
