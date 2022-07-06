using Microsoft.AspNetCore.Mvc;
using OptimaxTest.API.Data.Model;
using OptimaxTest.API.Services.Interfaces;

namespace OptimaxTest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("GetAppUserByAppUserID/{appUserID}")]
        public async Task<ActionResult<AppUser?>> GetUserByUserID(int appUserID)
        {
            try
            {

                AppUser? appUser = await _userService.GetAppUserByAppUserID(appUserID);
                return appUser != null ? Ok(appUser) : NotFound(appUserID);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpGet]
        [Route("GetAllAppUsers")]
        public async Task<ActionResult<List<AppUser>>> GetAllAppUsers()
        {
            try
            {
                var users = await _userService.GetAllAppUsers();
                return users != null ? Ok(users) : NotFound(users);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("CheckAppUserPermissionByPermissionID/{appUserID}&{permissionID}")]
        public async Task<ActionResult<bool>> CheckAppUserPermissionByPermissionID(int appUserID, int permissionID)
        {
            try
            {
                return await _userService.CheckAppUserPermissionByPermissionID(appUserID, permissionID);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("InsertNewAppUser/{username}&{password}&{firstName}&{surname}&{userRoleId}&{isActive}")]
        public async Task<ActionResult<AppUser?>> InsertNewAppUser(string username, 
            string password,
            string firstName,
            string surname,
            int userRoleId,
            bool isActive)
        {
            try
            {
                var user = await _userService.InsertNewAppUser(username, password, firstName,surname,userRoleId,isActive);
                return user != null ? Ok(user) : BadRequest(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("InsertNewAppUser")]
        public async Task<ActionResult<AppUser?>> InsertNewAppUser([FromBody] AppUser appUser)
        {
            try
            {
                var user = await _userService.InsertNewAppUser(appUser);
                return user != null ? Ok(user) : BadRequest(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
