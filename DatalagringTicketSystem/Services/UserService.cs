using Microsoft.EntityFrameworkCore;
using DatalagringTicketSystem.Models;
using DatalagringTicketSystem.Contexts;

namespace DatalagringTicketSystem.Services
{
    public class UserService
    {
        private static DataContext _context = new DataContext();
        //Metod för att hitta användare baserat på Email
        internal async Task<UserModel> GetUserByEmailAsync(string email)
        {
            var _user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
            if (_user != null)
                return new UserModel
                {
                    UserId = _user.UserId,
                    FirstName = _user.FirstName,
                    LastName = _user.LastName,
                    Email = _user.Email,
                    PhoneNumber = _user.PhoneNumber,
                };

            else
                return null!;
        }
        //Metod för att hitta användare baserat på Id
        internal async Task<UserModel> GetUserByIdAsync(Guid UserId)
        {
            var userEntity = await _context.Users.FindAsync(UserId);

            if (userEntity != null)
            {
                return new UserModel
                {
                    UserId = userEntity.UserId,
                    FirstName = userEntity.FirstName,
                    LastName = userEntity.LastName,
                    Email = userEntity.Email,
                    PhoneNumber = userEntity.PhoneNumber
                };
            }

            return null;
        }
        //Skapa ny användare
        internal async Task<UserModel> CreateUserAsync()
        {
            var databaseService = new DatabaseService();
            var newUser = new UserModel();

            Console.WriteLine("Please type in the following information.");

            newUser.UserId= Guid.NewGuid();

            Console.Write("First Name: ");
            newUser.FirstName = Console.ReadLine() ?? "";

            Console.Write("Last Name: ");
            newUser.LastName = Console.ReadLine() ?? "";

            Console.Write("Email: ");
            newUser.Email = Console.ReadLine() ?? "";

            Console.Write("PhoneNumber: ");
            newUser.PhoneNumber = Console.ReadLine() ?? "";

            await databaseService.SaveUserToDatabaseAsync(newUser);
            Console.WriteLine("User has been saved.");

            return newUser;
        }
    }
}
