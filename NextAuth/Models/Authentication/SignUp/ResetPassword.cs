using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace NextAuth.Models.Authentication.SignUp
{

    public class ResetPassword
    {
        [Required]
        public string Password { get; set; } = null!;

        [Compare("Password",ErrorMessage ="The password and Confirmation password dont not match.")]
        public string ConfirmPassword { get; set; } = null!;

        public string Email { get; set; }=null!;
        public string Token { get; set; }=null!;
    }
}
