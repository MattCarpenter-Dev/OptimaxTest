using System;
using System.Collections.Generic;

namespace OptimaxTest.API.Data.Model
{
    public partial class UserRolePermission
    {
        public int UserRolePermissionId { get; set; }
        public int? UserRoleId { get; set; }
        public int? PermissionId { get; set; }
        public bool IsActive { get; set; }

        public virtual Permission? Permission { get; set; }
        public virtual UserRole? UserRole { get; set; }
    }
}
