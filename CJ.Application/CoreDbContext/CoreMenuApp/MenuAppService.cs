using System;
using System.Collections.Generic;
using System.Text;
using CJ.Data.NetCoreModels;
using CJ.Repositories.BaseRepositories;

namespace CJ.Application.CoreDbContext.CoreMenuApp
{
    public class MenuAppService : IMenuAppService
    {
        private readonly IRepository<CoreMenu> _repository;
        public MenuAppService(IRepository<CoreMenu> repository)
        {
            _repository = repository;
        }
        public List<CoreMenu> GetMenus()
        {
            return _repository.GetAllList();
        }
    }
}
