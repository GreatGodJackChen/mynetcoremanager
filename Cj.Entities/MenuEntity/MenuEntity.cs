using System;
using System.Collections.Generic;
using System.Text;

namespace CJ.Entities.MenuEntity
{
    public class MenuEntity
    {
        public MenuEntity()
        {
            Children = new List<MenuEntity>();
        }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public string Component { get; set; }
        public string ParentId { get; set; }
        public string Icon { get; set; }
        public int? Level { get; set; }

        public int? Sort { get; set; }
        public List<MenuEntity> Children { get; set; }
        public string key { get; set; }
    }
}
