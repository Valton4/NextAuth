using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NextAuth.Data;
using NextAuth.Models;
using System.Globalization;

namespace NextAuth.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly Context _con;
        public AdminController(UserManager<IdentityUser> userManager,
            Context con)
        {
            _userManager = userManager;
            _con = con;
        }

        [HttpGet("GetLoggedInUser")]
        public async Task<IActionResult> GetLoggedInUser()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                return Ok(new { user,userRoles });
            }
            return StatusCode(StatusCodes.Status400BadRequest,
                   new Response { Status = "Error", Message = $"User not loggedIn" });



        }
    }
}



