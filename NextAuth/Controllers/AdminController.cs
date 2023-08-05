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
                return Ok(new { user, userRoles });
            }
            return StatusCode(StatusCodes.Status400BadRequest,
                   new Response { Status = "Error", Message = $"User not loggedIn" });
        }
        [AllowAnonymous]
        [HttpGet("getPublications")]
        public async Task<ProfessorPublication[]> getPublications()
        {

            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user != null)
            {

                var publications = await _con.ProfessorPublications.Where(x => x.ProfessorId == 1).ToArrayAsync();
                return publications;
            }

            return null;

        }
        [AllowAnonymous]
        [HttpPost("postPublication")]
        public async Task<IActionResult> postPublication([FromForm] ProfessorPublicationDetail publications)
        {
            if (publications != null)
            {

                var PPublications = new ProfessorPublication()
                {
                    ProfessorId = publications.Publication.ProfessorId,
                    IsActive = publications.Publication.IsActive,
                    CreatedAt = publications.Publication.CreatedAt,
                    UpdatedAt = publications.Publication.UpdatedAt
                };
                var publicationDetails = new ProfessorPublicationDetail()
                {
                    PublicationId = publications.PublicationId,
                    PublicationFormId = publications.PublicationFormId,
                    Value = publications.Value,
                    IsActive = publications.IsActive
                };
                await _con.ProfessorPublications.AddAsync(PPublications);
                await _con.ProfessorPublicationDetails.AddAsync(publicationDetails);
                await _con.SaveChangesAsync();
                return Ok(publications);
            }

            return StatusCode(StatusCodes.Status400BadRequest,
                   new Response { Status = "Error", Message = $"Something went wrong" });

        }

    }
}



