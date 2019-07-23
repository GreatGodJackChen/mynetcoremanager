using CJ.Data.NetCoreModels;
using CJ.Entities.NetCore;
using CJ.Repositories.BaseRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace CJ.Application.CoreDbContext.CorePermissionApp
{
    public class PermissionAppService : IPermissionAppService
    {
        private readonly IRepository<CorePermission> _repository;
        private readonly IRepository<CoreMenu> _menuRepository;
        public PermissionAppService(IRepository<CorePermission> repository, IRepository<CoreMenu> menuRepository)
        {
            _repository = repository;
            _menuRepository = menuRepository;
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
        public PaginatedList<PermissionEntity> GetList(int currentPage, int pageSize, Expression<Func<PermissionEntity, bool>> predicate)
        {
            var query = from o in _repository.GetAll()
                        join d in _menuRepository.GetAll()
                        on o.MenuId equals d.Id
                        select new PermissionEntity()
                        {
                            Id = o.Id,
                            Name = o.Name,
                            ActionCode = o.ActionCode,
                            MenuId = o.MenuId,
                            MenuName = d.Name,
                            CreatedTime = o.CreatedTime,
                            Status = o.Status
                        };


            if (predicate != null)
            {
                query = query.Where(predicate);
            }
           var tt=  PaginatedList<PermissionEntity>.CreatePage(query,currentPage, pageSize);
            //var result = _repository.FindListPage(currentPage, pageSize, _repository.GetAll());
            return tt;
        }
        public PaginatedList<PermissionEntity> Update(int currentPage, int pageSize, Expression<Func<PermissionEntity, bool>> predicate, CorePermission permission)
        {
            _repository.UpdateColumn(permission, null, new string[] { "Name", "MenuId", "ActionCode" });
            var result = GetList(currentPage, pageSize, predicate);
            return result;
        }
        public PaginatedList<CorePermission> Delete(int currentPage, int pageSize, string[] strId)
        {
            foreach (var id in strId)
            {
                _repository.Delete(id);
            }
            var result = _repository.FindListPage(currentPage, pageSize,null);
            return result;
        }
    }
}
