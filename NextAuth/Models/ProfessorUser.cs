using Microsoft.AspNetCore.Identity;

namespace NextAuth.Models
{
    public class ProfessorUser : IdentityUser
    {
        public int ProfessorId { get; set; }
    }
}
