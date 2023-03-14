using Microsoft.EntityFrameworkCore;
using DatalagringTicketSystem.Models;
using DatalagringTicketSystem.Contexts;

namespace DatalagringTicketSystem.Services
{
    public class UserService
    {
        private static DataContext _context = new DataContext();

        internal async Task<UserModel> GetUserByEmailAsync(string email)
        {
            var _user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
            if (_user != null)
                return new UserModel
                {   Id  = _user.Id,
                    FirstName= _user.FirstName,
                    LastName= _user.LastName,
                    Email = _user.Email,
                    PhoneNumber = _user.PhoneNumber,
                };

            else
                return null!;
        }
    }

}
