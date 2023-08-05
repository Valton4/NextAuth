using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NextAuth.Data;
using NextAuth.ViewModels;
using User.Managment.Service.Services;

namespace NextAuth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfessorPublicationsController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly Context _con;
        private readonly UserWithRoles _user;
        public ProfessorPublicationsController(UserManager<IdentityUser> userManager,
            Context con, UserWithRoles user)
        {
            _userManager = userManager;
            _con = con;
            _user = user;
        }
        [HttpGet("Index")]
        public async Task<IActionResult> Index()
        {
            GetLoggedInUser g = new GetLoggedInUser(_userManager,_con);
            var user = await g.getLoggedInUser(User.Identity.Name);
            if (user != null)
                return Ok();

            return BadRequest();
        }

    }
}
