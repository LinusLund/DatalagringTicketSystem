using DatalagringTicketSystem.Contexts;
using DatalagringTicketSystem.Models;
using DatalagringTicketSystem.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Net.Sockets;


namespace DatalagringTicketSystem.Services
{
    internal class DatabaseService
    {
        private readonly DataContext _context;

        public DatabaseService()
        {
            _context = new DataContext();
        }

        public async Task SaveTicketToDatabaseAsync(TicketModel ticket)
        {
            TicketEntity ticketEntity = ticket;
            _context.Add(ticketEntity);
            await _context.SaveChangesAsync();
        }

        public async Task SaveUserToDatabaseAsync(UserModel newUser)
        {
            UserEntity userEntity = newUser;
            _context.Add(userEntity);
            await _context.SaveChangesAsync();
        }






    }
}