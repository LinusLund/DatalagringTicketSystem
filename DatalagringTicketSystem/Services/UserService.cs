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
            if (email == null)
            {
                throw new ArgumentNullException(nameof(email));
            }

            var userEntity = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
            return userEntity;
        }
        //Metod för att hitta användare baserat på Id
        internal async Task<UserModel> GetUserByIdAsync(Guid UserId)
        {    
            if (UserId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(UserId));
            }

            var userEntity = await _context.Users.FirstOrDefaultAsync(x=>x.UserId== UserId);
            return userEntity;
             
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
