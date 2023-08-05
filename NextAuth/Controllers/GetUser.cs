using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NextAuth.Models;
using NextAuth.ViewModels;

namespace NextAuth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetUser : ControllerBase
    {
        protected readonly UserManager<ProfessorUser> _userManager;
        public GetUser(UserManager<ProfessorUser> userManager)
        {
            _userManager = userManager;
        }

        protected async Task<UserWithRoles> getLoggedInUser()
        {
            ProfessorUser user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                UserWithRoles userWithRoles = new UserWithRoles()
                {
                    User = user,
                    UserRoles = userRoles
                };

                return userWithRoles;
            }
            return null;
        }
    }
}
