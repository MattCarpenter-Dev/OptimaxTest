namespace OptimaxTest.API.Data.Model
{
    public partial class UserRole
    {
        public UserRole()
        {
            UserRolePermissions = new HashSet<UserRolePermission>();
        }

        public int UserRoleId { get; set; }
        public string UserRoleName { get; set; } = null!;
        public bool IsActive { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public DateTime? DateTimeDeacticated { get; set; }

        public virtual ICollection<UserRolePermission> UserRolePermissions { get; set; }
    }
}
