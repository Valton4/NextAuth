using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NextAuth.Models;
using NextAuth.Models.Authentication.Login;
using NextAuth.Models.Authentication.SignUp;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using User.Managment.Service.Models;
using User.Managment.Service.Services;


namespace NextAuth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {

        private readonly UserManager<ProfessorUser> _userManager;
        private readonly SignInManager<ProfessorUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleMenager;
        private readonly IEmailService _emailS;
        private readonly IConfiguration _configuration;
        public AuthenticationController(UserManager<ProfessorUser> userManager,
            RoleManager<IdentityRole> roleManager, SignInManager<ProfessorUser> signInManager, IConfiguration configuration, IEmailService emailService)
        {
            _userManager = userManager;
            _roleMenager = roleManager;
            _configuration = configuration;
            _emailS = emailService;
            _signInManager = signInManager;
        }

        [HttpGet("getRoles")]
        public async Task<IActionResult> getRoles()
        {
            var roles = await _roleMenager.Roles.ToArrayAsync();
            if (roles != null || roles?.Length > 0)
            {
                return StatusCode(StatusCodes.Status200OK,
                       new Response { Status = "Success", Message = "Roles retrieved successfully" });

            }
            return StatusCode(StatusCodes.Status400BadRequest,
                     new Response { Status = "Error", Message = $"No role exsists in your website" });

        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterUser registerUser, string role)
        {
            //Check If User Exist
            var userExist = await _userManager.FindByEmailAsync(registerUser.Email);
            if (userExist != null)
            {
                return StatusCode(StatusCodes.Status403Forbidden,
                    new Response { Status = "Error", Message = "User already Exist" });
            }
            //Add new User
            ProfessorUser user = new()
            {
                Email = registerUser.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = registerUser.Username,
                TwoFactorEnabled = true,
                ProfessorId = registerUser.ProfessorId
            };

            if (await _roleMenager.RoleExistsAsync(role))
            {
                var result = await _userManager.CreateAsync(user, registerUser.Password);
                if (!result.Succeeded)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        new Response { Status = "Error", Message = "User Failed to Create" });
                }
                //Add Role to the User
                await _userManager.AddToRoleAsync(user, role);
                //Add Token to Verify the Email...
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var confirmationLink = Url.Action(nameof(ConfirmEmail), "Authentication",
                    new { token, email = user.Email }, Request.Scheme);
                var message = new Message(new string[] { user.Email! }, "Confirmation email link", confirmationLink!);
                _emailS.SendEmail(message);

                return StatusCode(StatusCodes.Status200OK,
                    new Response { Status = "Success", Message = $"User Created & Email Sent to {user.Email} Successfully" });
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                       new Response { Status = "Error", Message = "The Role doesn`t Exist" });
            }

        }

        [HttpGet]
        public async Task<IActionResult> TestEmail()
        {
            var message = new Message

                (new string[] { "arditgrajqevci@gmail.com" },
                "test", "<h1>Testing..</h1>");

            _emailS.SendEmail(message);

            return StatusCode(StatusCodes.Status200OK,
                new Response { Status = "Success", Message = "Email " });
        }



        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user != null)
            {
                var result = await _userManager.ConfirmEmailAsync(user, token);
                if (result.Succeeded)
                {
                    return StatusCode(StatusCodes.Status200OK,
                        new Response { Status = "Success", Message = "Email Sent SuccessFully" });
                }
            }
            return StatusCode(StatusCodes.Status500InternalServerError,
                new Response { Status = "Error", Message = "This User doesn`t exist!" });
        }


        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel login)
        {
            var user = await _userManager.FindByEmailAsync(login.Email);

            //Checking the user and  Password
            if (user != null && await _userManager.CheckPasswordAsync(user, login.Password))
            {
                //Claimlist Create
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                };

                //We add Roles to the Clam

                var userRoles = await _userManager.GetRolesAsync(user);
                foreach (var role in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, role));
                }



                //Generate the Token with Claims
                var jwtToken = GetToken(authClaims);
                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                    expiration = jwtToken.ValidTo
                });

                //Returning the Token

            }
            return Unauthorized();
        }

        [HttpPost]
        [Route("login-2FA")]
        public async Task<IActionResult> LoginWithOTP(string code, string username)
        {
            var user = await _userManager.FindByNameAsync(username);

            var signIn = _signInManager.TwoFactorSignInAsync("Email", code, false, false);

            //Checking the user and  Password
            if (user != null)
            {

                //Claimlist Create
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                };

                //We add Roles to the Clam

                var userRoles = await _userManager.GetRolesAsync(user);
                foreach (var role in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, role));
                }



                //Generate the Token with Claims
                var jwtToken = GetToken(authClaims);
                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                    expiration = jwtToken.ValidTo
                });

                //Returning the Token

            }

            return StatusCode(StatusCodes.Status404NotFound,
               new Response { Status = "Success", Message = "Invalid Code" });

        }

        [HttpPost]
        [Route("Forgot-Password")]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword([Required] string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var forgotPasswordLink = Url.Action(nameof(ResetPassword), "Authentication", new { token, email = user.Email }, Request.Scheme);
                var message = new Message(new string[] { user.Email! }, "Forgot Password Link", forgotPasswordLink!);
                _emailS.SendEmail(message);

                return StatusCode(StatusCodes.Status200OK,
                      new Response { Status = "Success", Message = $"Password change request is sent on Email {user.Email}. Please Open the link in your Email" });
            }
            return StatusCode(StatusCodes.Status400BadRequest,
                     new Response { Status = "Error", Message = $"Couldn`t send link to Email, please Try Agian." });

        }

        [HttpGet("reset-Password")]
        public async Task<IActionResult> ResetPassword(string token, string email)
        {
            var model = new ResetPassword { Token = token, Email = email };
            return Ok(new
            {
                model
            });
        }

        [HttpPost]
        [Route("Reset-Password")]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(ResetPassword resetPassword)
        {
            var user = await _userManager.FindByEmailAsync(resetPassword.Email);
            if (user != null)
            {
                var resetPassResult = await _userManager.ResetPasswordAsync(user, resetPassword.Token, resetPassword.Password);
                if (resetPassResult.Succeeded)
                {
                    foreach (var error in resetPassResult.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                    return Ok(ModelState);
                }

                return StatusCode(StatusCodes.Status200OK,
                new Response { Status = "Success", Message = $"Password has been changed" });
            }
            return StatusCode(StatusCodes.Status400BadRequest,
                     new Response { Status = "Error", Message = $"Couldn`t send link to Email, please Try Agian." });

        }

        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );
            return token;
        }


    }
}
