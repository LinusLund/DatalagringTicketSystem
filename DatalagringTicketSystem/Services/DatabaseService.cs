using DatalagringTicketSystem.Contexts;
using DatalagringTicketSystem.Models;
using DatalagringTicketSystem.Models.Entities;
using Microsoft.EntityFrameworkCore;



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

        public async Task<IEnumerable<TicketModel>> GetAllTicketsAsync()
        {
            var ticketEntities = await _context.Tickets.ToListAsync();
            var ticketModels = new List<TicketModel>();
            var userService = new UserService();

            foreach (var ticketEntity in ticketEntities)
            {
                var userModel = await userService.GetUserByIdAsync(ticketEntity.Id);

                if (userModel != null)
                {
                    TicketModel ticketModel = ticketEntity;
                    ticketModel.UserEmail = userModel.Email;
                    ticketModels.Add(ticketModel);
                }
            }

            return ticketModels;
        }





    }
}