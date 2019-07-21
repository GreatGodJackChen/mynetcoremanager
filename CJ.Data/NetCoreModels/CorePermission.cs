using Cj.Entities.BaseEntity;
using System;
using System.Collections.Generic;

namespace CJ.Data.NetCoreModels
{
    public partial class CorePermission:Entity
    {
        //public string Id { get; set; }
        public string MenuId { get; set; }
        public string Name { get; set; }
        public string ActionCode { get; set; }
        public string Icon { get; set; }
        public string Description { get; set; }
        public int? Status { get; set; }
        public int? IsDeleted { get; set; }
        public int? Type { get; set; }
        public string CreatedByUserId { get; set; }
        public DateTime? CreatedTime { get; set; }
        public string CreatedByUserName { get; set; }
        public DateTime? ModifiedTime { get; set; }
        public string ModifiedByUserId { get; set; }
        public string ModifiedByUserName { get; set; }
    }
}
