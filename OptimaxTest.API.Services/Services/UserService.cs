using Microsoft.EntityFrameworkCore;
using OptimaxTest.API.Data.Model;
using OptimaxTest.API.Services.Interfaces;

namespace OptimaxTest.API.Services.Services
{
    public class UserService : IUserService
    {

        private readonly OptimaxDeveloperTestContext _context;
        public UserService(OptimaxDeveloperTestContext context)
        {
            _context = context;
        }

        public async Task<AppUser?> GetAppUserByAppUserID(int appUserId)
        {
            //List<AppUser> usersList = await _context.AppUsers.FromSqlRaw("EXEC GetAppUserByAppUserID {0}", ID).ToListAsync();
            //return usersList.FirstOrDefault();

            var user = await _context.AppUsers
                .Where(x => x.AppUserId == appUserId)
                .FirstOrDefaultAsync();

            return user;
        }

        public async Task<IEnumerable<AppUser>> GetAllAppUsers()
        {
            return await _context.AppUsers.ToListAsync();
        }

        public async Task<bool> CheckAppUserPermissionByPermissionID(int appUserId, int permissionId)
        {
            // Get the user so we know their UserRoleId, ideally this would be passed to the controller.
            AppUser? user = await GetAppUserByAppUserID(appUserId);

            if (user == null) return false;

            var userRolePermission = await _context.UserRolePermissions
                .Where(x => x.UserRoleId == user.UserRoleId && x.PermissionId == permissionId)
                .FirstOrDefaultAsync();

            if (userRolePermission == null) return false;

            // Assumed if "IsActive" is null then it is active. I would convert this to non-nullable, disable Allow nulls and add a default of false in SQL.
            return userRolePermission.IsActive;
        }

        public async Task<AppUser> InsertNewAppUser(string username, string password, string firstName, string surname, int userRoleId, bool isActive)
        {
            // Check if user already exists.
            var user = await _context.AppUsers
                .Where(x => x.Username.ToUpper() == username.ToUpper())
                .FirstOrDefaultAsync();

            // User already exists.
            if (user != null) throw new Exception($"A user with the username \"{username}\" already exists.");

            AppUser appUser = new AppUser()
            {
                Username = username,
                PasswordHash = System.Text.Encoding.Unicode.GetBytes(password),
                FirstName = firstName,
                Surname = surname,
                UserRoleId = userRoleId,
                IsActive = isActive,
                DateTimeCreated = DateTime.Now
            };

            await _context.AppUsers
                .AddAsync(appUser);

            appUser.AppUserId = await _context.SaveChangesAsync();

            return appUser;
        }

        // Ideally DTOs (Data transfer object(s)) would be used so that unneccassary properties are not exposed, such as AppUserId on creation.
        public async Task<AppUser> InsertNewAppUser(AppUser appUser)
        {
            // Prevent a duplicate being inserted.
            if (appUser.AppUserId > 0) return null;

            // Check if user already exists.
            var user = await _context.AppUsers
                .Where(x => x.Username.ToUpper() == appUser.Username.ToUpper()) // Unsure whether to add "&& IsActive == true" here.
                .FirstOrDefaultAsync();

            // User already exists.
            if (user != null) throw new Exception($"A user with the username \"{appUser.Username}\" already exists.");

            appUser.DateTimeCreated = DateTime.Now;

            await _context.AppUsers
                .AddAsync(appUser);

            appUser.AppUserId = await _context.SaveChangesAsync();

            return appUser;
        }
    }
}
