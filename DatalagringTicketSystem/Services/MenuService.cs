using DatalagringTicketSystem.Models;
using DatalagringTicketSystem.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;


namespace DatalagringTicketSystem.Services
{
    internal class MenuService
    {


        public async Task CreateNewTicketAsync()
        {
            var databaseService = new DatabaseService();
            var userService = new UserService();
            var newUser = new UserModel();

            Console.WriteLine("Do you have an existing account? (yes or no)");

            string input = Console.ReadLine().ToLower();
            if (input == "no")
            {
                Console.WriteLine("Please type in the following information.");

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
            }
            else if (input == "yes")
            {
                Console.WriteLine("Please enter your email.");
                string email = Console.ReadLine();

                var user = await userService.GetUserByEmailAsync(email);
                if (user == null)
                {
                    Console.WriteLine($"User with email {email} does not exist in the database.");
                    return;
                }
                else
                {
                    // Create new ticket with user ID set to user ID
                    var ticket = new TicketModel { Id = user.Id };

                    Console.Write("Describe your problem: ");
                    ticket.Description = Console.ReadLine();

                    ticket.Status = TicketStatus.EjPåbörjad;

                    ticket.DateCreated = DateTime.Now;

                    Console.Clear();
                    Console.WriteLine("Ticket has been created!");

                    // Save ticket to database
                    await databaseService.SaveTicketToDatabaseAsync(ticket);
                }
            }
            else
            {
                Console.WriteLine("Invalid input.");
                return;
            }
        }

        /* public static async Task ShowAllTicketsAsync()
         {
             var databaseService = new DatabaseService();
             var ticketService = new TicketService();

         }*/
    }
}

