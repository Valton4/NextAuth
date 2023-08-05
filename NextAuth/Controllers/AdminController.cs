using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using NextAuth.Data;
using NextAuth.EPublication;
using NextAuth.Migrations;
using NextAuth.Models;
using System.Globalization;

namespace NextAuth.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : GetUser
    {
        private readonly Context _con;
        public AdminController(UserManager<ProfessorUser> userManager,
            Context con) : base(userManager)
        {
            _con = con;
        }

        [HttpGet("GetLoggedInUser")]
        public async Task<IActionResult> GetLoggedInUser()
        {
            return Ok(await base.getLoggedInUser());
        }

    }
}



