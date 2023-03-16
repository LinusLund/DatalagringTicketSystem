﻿using DatalagringTicketSystem.Contexts;
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
                var userModel = await userService.GetUserByIdAsync(ticketEntity.UserId);

                if (userModel != null)
                {
                    TicketModel ticketModel = ticketEntity;
                    ticketModel.UserEmail = userModel.Email;
                    ticketModels.Add(ticketModel);
                }
            }

            return ticketModels;
        }

        public async Task<TicketEntity> GetAsync(int ticketNumber)

        {
            var ticket = await _context.Tickets
                    .FirstOrDefaultAsync(t => t.TicketNumber == ticketNumber);
                return ticket;
        }
        //Tog hjälp av Chat GPT för att lösa problemet med att contexten lagrade samma entitet två gånger.
        //Nu kollar den först om ticketen är sparad eller inte.
        public async Task UpdateAsync(TicketModel ticket)
        {

            var existingTicket = await _context.Tickets.FindAsync(ticket.TicketNumber);
            if (existingTicket != null)
            {
                existingTicket.Status = (int)ticket.Status;
                await _context.SaveChangesAsync();
            }
            else
            {
                _context.Tickets.Attach(ticket);
                _context.Entry(ticket).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
        }
    }
}