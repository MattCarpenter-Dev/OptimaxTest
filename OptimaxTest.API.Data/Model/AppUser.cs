namespace OptimaxTest.API.Data.Model
{
    public partial class AppUser
    {
        public int AppUserId { get; set; }
        public string Username { get; set; } = null!;
        public byte[] PasswordHash { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public int UserRoleId { get; set; }
        public bool IsActive { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public DateTime? DateTimeDeactivated { get; set; }
    }
}
