using CJ.Data.NetCoreModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace CJ.Entities.NetCore
{
    public class PermissionEntity
    {
        public string Id { get; set; }
        public string MenuId { get; set; }
        public string Name { get; set; }
        public string ActionCode { get; set; }
        public int? Status { get; set; }
        public string MenuName { get; set; }

        public DateTime? CreatedTime { get; set; }
    }
}
