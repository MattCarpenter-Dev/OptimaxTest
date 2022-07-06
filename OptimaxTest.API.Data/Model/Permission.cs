using System;
using System.Collections.Generic;

namespace OptimaxTest.API.Data.Model
{
    public partial class Permission
    {
        public Permission()
        {
            UserRolePermissions = new HashSet<UserRolePermission>();
        }

        public int PermissionId { get; set; }
        public string PermissionName { get; set; } = null!;
        public bool IsActive { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public DateTime? DateTimeDeacticated { get; set; }

        public virtual ICollection<UserRolePermission> UserRolePermissions { get; set; }
    }
}
