using Microsoft.AspNetCore.Identity;

namespace NextAuth.ViewModels
{
    public class UserWithRoles
    {
        public IdentityUser User { get; set; }
        public List<string> UserRoles { get; set; }
    }
}
