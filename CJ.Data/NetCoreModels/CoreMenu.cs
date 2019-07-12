using Cj.Entities.BaseEntity;
using System;
using System.Collections.Generic;

namespace CJ.Data.NetCoreModels
{
    public partial class CoreMenu:Entity
    {
       // public string Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public string Component { get; set; }
        public string Icon { get; set; }
        public string Controll { get; set; }
        public string ParentGuid { get; set; }
        public string ParentName { get; set; }
        public int? Level { get; set; }
        public string Description { get; set; }
        public int? Sort { get; set; }
        public int? IsDefaultRouter { get; set; }
        public DateTime? CreatedTime { get; set; }
        public string CreatedByUserGuid { get; set; }
        public string CreatedByUserName { get; set; }
        public DateTime? ModifiedTime { get; set; }
        public string ModifiedByUserGuid { get; set; }
        public string ModifiedByUserName { get; set; }
        public int? IsLocked { get; set; }
        public int? IsDeleted { get; set; }
        public int? NotCache { get; set; }
        public string BeforeCloseFun { get; set; }

    }
}
