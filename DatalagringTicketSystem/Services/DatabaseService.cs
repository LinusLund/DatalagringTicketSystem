using DatalagringTicketSystem.Contexts;
using DatalagringTicketSystem.Models;
using DatalagringTicketSystem.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace TicketApp.Services
{
    internal class DatabaseService
    {
        private readonly DataContext _context;

        public DatabaseService()
        {
            _context = new DataContext();
        }

        public async Task SaveToDatabaseAsync(TicketModel ticket)
        {
            TicketEntity ticketEntity = ticket;
            _context.Add(ticketEntity);
            await _context.SaveChangesAsync();
        }

 

        public async Task<IEnumerable<TicketModel>> GetAll()
        {
  
        }


    }
}