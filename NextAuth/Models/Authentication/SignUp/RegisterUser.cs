﻿using System.ComponentModel.DataAnnotations;

namespace NextAuth.Models.Authentication.SignUp
{
    public class RegisterUser
    {
        [Required(ErrorMessage = "Username is Required")]
        public string? Username { get; set; }
        [EmailAddress]
        [Required(ErrorMessage = "Email is Required")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Password is Required")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "ProfessorId is Required")]
        public int ProfessorId { get; set; }
    }
}
