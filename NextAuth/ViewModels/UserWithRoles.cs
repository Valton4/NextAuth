using Microsoft.AspNetCore.Identity;
using NextAuth.Models;

namespace NextAuth.ViewModels
{
    public class UserWithRoles
    {
        public ProfessorUser User { get; set; }
        public IList<string>? UserRoles { get; set; }
    }
}
