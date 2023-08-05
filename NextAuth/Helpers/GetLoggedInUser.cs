using Microsoft.AspNetCore.Identity;
using NextAuth.Data;
using NextAuth.Models;
using NextAuth.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User.Managment.Service.Services
{
    public class GetLoggedInUser
    {
        private readonly UserManager<ProfessorUser> _userManager;
        private readonly Context _con;
        public GetLoggedInUser(UserManager<ProfessorUser> userManager,
            Context con)
        {
            _userManager = userManager;
            _con = con;
        }
        public async Task<UserWithRoles> getLoggedInUser(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                UserWithRoles userWithRoles = new()
                {
                    User = user,
                    UserRoles = userRoles.ToList()
                };
                return userWithRoles;

            }

            return null;
        }
    }
}
