using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NextAuth.Data;
using NextAuth.EPublication;
using NextAuth.Models;
using NextAuth.ViewModels;
using System.Collections.Immutable;
using User.Managment.Service.Services;

namespace NextAuth.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProfessorPublicationsController : GetUser
    {
        private readonly Context _con;
        public ProfessorPublicationsController(UserManager<ProfessorUser> userManager,
            Context con) : base(userManager)
        {
            _con = con;
        }
        [HttpGet("Index")]
        public async Task<IActionResult> Index()
        {
            var user = await base.getLoggedInUser();
            if (user != null)
                return Ok(user);

            return BadRequest();
        }
        [HttpGet("getPublications")]
        public async Task<IActionResult> getPublications()
        {

            var user = await base.getLoggedInUser();
            if (user != null)
            {
                var publications = await _con.ProfessorPublications
                    .Include(x => x.ProfessorPublicationDetails)
                    .Where(x => x.ProfessorId == user.User.ProfessorId).ToArrayAsync();
                return Ok(publications);
            }

            return Ok();
        }
        [HttpGet("getPublicationForms")]
        public async Task<ActionResult<ProfessorPublicationForm[]>> getPublicationForms()
        {
            var user = await base.getLoggedInUser();
            if (user != null)
            {

                var publications = await _con.ProfessorPublicationForms.ToArrayAsync();
                return publications;
            }

            return StatusCode(StatusCodes.Status400BadRequest,
                         new Response { Status = "Error", Message = $"Something went Wrong!!!" });

        }

        [HttpPost("postPublication")]
        public async Task<IActionResult> postPublication(ProfessorPublicationDetail publications)
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
                   new Response { Status = "Error", Message = $"Something went Wrong!!!" });
        }
        [HttpGet("GetPublicationTypes")]
        public async Task<ActionResult<ProfessorPublicationType[]>> GetPublicationTypes()
        {
            var user = await base.getLoggedInUser();
            if (user == null)
                return StatusCode(StatusCodes.Status400BadRequest,
                   new Response { Status = "Error", Message = $"Something went Wrong!!!" });

            var data = await _con.ProfessorPublicationTypes.ToArrayAsync();
            if (data.Length != 0)
                return data;
            return StatusCode(StatusCodes.Status400BadRequest,
                  new Response { Status = "Error", Message = $"Something went Wrong!!!" });
        }

        [HttpGet("getPublicationsFormsByType")]
        public async Task<ActionResult<ProfessorPublicationForm[]>> getPublicationsFormsByType(int id)
        {
            var user = await base.getLoggedInUser();
            if (user == null)
                return StatusCode(StatusCodes.Status400BadRequest,
                   new Response { Status = "Error", Message = $"Something went Wrong!!!" });
            var response = await _con.ProfessorPublicationForms
                .Where(x => x.PublicationTypeId == id)
                .ToArrayAsync();
            if (response.Length != 0)
                return Ok(response);

            return StatusCode(StatusCodes.Status400BadRequest,
                    new Response { Status = "Error", Message = $"Something went Wrong!!!" });
        }
    }
}
