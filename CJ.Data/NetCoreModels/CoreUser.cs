using Cj.Entities.BaseEntity;
using System;
using System.Collections.Generic;

namespace CJ.Data.NetCoreModels
{
    public partial class CoreUser: Entity
    {
       // public string Id { get; set; }
        public string LoginName { get; set; }
        public string DisplayName { get; set; }
        public string Password { get; set; }
        public int? IsLocked { get; set; }
        public int? IsDeleted { get; set; }
        public DateTime? CreatedTime { get; set; }
        public string CreatedByUserGuid { get; set; }
        public string CreatedByUserName { get; set; }
        public DateTime? ModifiedTime { get; set; }
        public string ModifiedByUserGuid { get; set; }
        public string ModifiedByUserName { get; set; }
        public string Description { get; set; }
        public string UserType { get; set; }
    }
}
