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
                {
                    Email = _user.Email,
                };

            else
                return null!;
        }
    }

}
