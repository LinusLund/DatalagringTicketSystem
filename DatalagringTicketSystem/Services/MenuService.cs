using DatalagringTicketSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using TicketApp.Services;

namespace DatalagringTicketSystem.Services
{
    internal class MenuService
    {
        public async Task CreateNewTicketAsync()
        {
            var ticket = new TicketModel();

            Console.Write("E-mail of the person creating ticket: ");
            string email = Console.ReadLine() ?? "";

            // Check if User exists in database
            var userService = new UserService();

            var customer = await userService.GetUserByEmailAsync(email);
            if (customer == null)
            {
                Console.WriteLine($"User with email {email} does not exist in the database.");
                return;
            }

            Console.Write("Describe your problem: ");
            ticket.Description = Console.ReadLine();

            ticket.DateCreated = DateTime.Now;


            // Save ticket to database
            await DatabaseService.SaveToDatabaseAsync(ticket);
        }
        public async Task ListAllTicketAsync()
        {
            

        }

        public async Task ListSpecificTicketAsync()
        {

        }

        public async Task UpdateSpecificTicketAsync()
        {
           
        }

        public async Task DeleteSpecificTicketAsync()
        {
           

        }
    }
}

