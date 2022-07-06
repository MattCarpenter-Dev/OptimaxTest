using OptimaxTest.API.Data.Model;

namespace OptimaxTest.API.Services.Interfaces
{
    public interface IUserService
    {
        public Task<AppUser?> GetAppUserByAppUserID(int appUserId);

        public Task<IEnumerable<AppUser>> GetAllAppUsers();

        public Task<bool> CheckAppUserPermissionByPermissionID(int appUserID, int permissionID);

        public Task<AppUser> InsertNewAppUser(string username, string password, string firstName, string surname, int userRoleId, bool isActive);

        public Task<AppUser> InsertNewAppUser(AppUser appUser);
    }
}
