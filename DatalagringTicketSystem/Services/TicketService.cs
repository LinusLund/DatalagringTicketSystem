using DatalagringTicketSystem.Models;


namespace DatalagringTicketSystem.Services
{
    internal class TicketService
    {
        //Hämtar Ticket baserat på ID
        internal async Task<TicketModel?> GetTicketAsync()
        {
            var databaseService = new DatabaseService();
            Console.Clear();
            Console.Write("Write the ticket number you wish to find: ");
            if (!int.TryParse(Console.ReadLine(), out int ticketNumber))
            {
                Console.WriteLine("Invalid input. Please enter a valid ticket number.");
                return null;
            }

            var ticket = await databaseService.GetAsync(ticketNumber);

            if (ticket == null)
            {
                Console.WriteLine($"Ticket {ticket} not found.");
                return null;
            }
            //Kallar på GetCommentsAsync med det angivna ticket-Idt och lägger in kommentarer i en ICollection.
            var comments = await databaseService.GetCommentsAsync(ticketNumber);
            ticket.Comments = comments;

            return ticket;
        }

        
    }

 
}

