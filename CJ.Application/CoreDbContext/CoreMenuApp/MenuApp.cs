using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CJ.Data.NetCoreModels;
using CJ.Entities.MenuEntity;

namespace CJ.Application.CoreDbContext.CoreMenuApp
{
    public class MenuApp : IMenuApp
    {
        private readonly IMenuAppService _menuAppService;
        public MenuApp(IMenuAppService menuAppService)
        {
            _menuAppService = menuAppService;
        }
        public List<MenuEntity> GetMenus()
        {
            var menus = _menuAppService.GetMenus();
            var newmenus = menus.Select(p => new MenuEntity
            {
                Id = p.Id,
                Name = p.Name,
                ParentId = p.ParentGuid,
                Path = p.Path,
                Icon = p.Icon,
                Component = p.Component,
                Level = p.Level,
                Sort = p.Sort,
            }).ToList();
            var Rootnode = newmenus.Where(x => string.IsNullOrEmpty(x.ParentId)).OrderBy(x => x.Sort).ToList();
            foreach (var menu in Rootnode)
            {
                BuildMenuTree(menu, 0, newmenus);
                //BuildMenu(menu);
            }

            return Rootnode;
        }
        public void BuildMenu(MenuEntity menuEntity)
        {
            menuEntity.Id = "";
            menuEntity.ParentId = "";
            if (menuEntity.Children.Count > 0)
            {
                foreach (var item in menuEntity.Children)
                {
                    BuildMenu(item);
                }
            }
        }
        public void BuildMenuTree(MenuEntity node, int? level, List<MenuEntity> menus)
        {
            //找到全部node节点下的全部子节点
            List<MenuEntity> childs = menus.Where(m => m.ParentId == node.Id)
                                            .Select(m => new MenuEntity()
                                            {
                                                Id = m.Id,
                                                Name = m.Name,
                                                ParentId = m.ParentId,
                                                Path = m.Path,
                                                Icon = m.Icon,
                                                Component = m.Component,
                                                Level = m.Level,
                                                Sort = m.Sort
                                            }).OrderBy(p => p.Sort)
                                            .ToList();
            if (childs.Count > 0)
            {
                node.Children = childs;
                //节点深度
                node.Level = level + 1;
                for (int i = 0; i < childs.Count; i++)
                {
                    //递归调用创建子节点
                    BuildMenuTree(childs[i], node.Level, menus);
                }
            }
        }
        public List<MenuEntity> LoadMenuTree(List<CoreMenu> menus, string selectedGuid = null)
        {
            var temp = menus.Select(x => new MenuEntity
            {
                Id = x.Id,
                ParentId = string.IsNullOrEmpty(x.ParentGuid) ? "0" : x.ParentGuid,
                Name = x.Name,
                Path = x.Path,
                Component = x.Component,
            }).ToList();
            var tree = BuildTree(temp, selectedGuid);
            return tree;
        }
        public List<MenuEntity> BuildTree(List<MenuEntity> menus, string selectedGuid = null)
        {
            var lookup = menus.ToLookup(x => x.ParentId);

            List<MenuEntity> Build(string pid)
            {
                return lookup[pid]
                    .Select(x => new MenuEntity()
                    {
                        Id = x.Id,
                        ParentId = x.ParentId,
                        Children = Build(x.Id),
                        Component = x.Component ?? "/",
                        Name = x.Name,
                        Path = x.Path,
                    }).ToList();
            }

            var result = Build(selectedGuid);
            return result;
        }
    }
}
