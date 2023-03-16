using DatalagringTicketSystem.Models;
using DatalagringTicketSystem.Models.Entities;



namespace DatalagringTicketSystem.Services
{
    internal class MenuService
    {

        //Byggde först med förbehållet att Användaren redan finns i databasen. sen läste jag inmlämningsuppgiften 
        //lite mer noggrannt och såg att man skulle spara användar information också. så fick bygga till det.
        public async Task CreateNewTicketAsync()
        {
            // Skapa instanser av våra service-klasser
            var databaseService = new DatabaseService();
            var userService = new UserService();

            // Fråga användaren om hen har ett befintligt konto
            Console.WriteLine("Do you have an existing account? (yes or no)");
            string input = Console.ReadLine().ToLower();

            //deklararer user som null för att det ska följa med genom hela metoden.Oavsett vad användaren väljer för alternativ.
            UserModel user = null;

            if (input == "no")
            {
                // Om användaren inte har ett konto, skapa ett nytt konto med hjälp av userService
                user = await userService.CreateUserAsync();
            }
            else if (input == "yes")
            {
                // Om användaren har ett konto, be hen att ange sin email och hämta användaren från databasen med hjälp av userService
                Console.WriteLine("Please enter your email.");
                string email = Console.ReadLine();
                user = await userService.GetUserByEmailAsync(email);

                if (user == null)
                {
                    // Om användaren inte finns i databasen, returnera ett felmeddelande
                    Console.WriteLine($"User with email {email} does not exist in the database.");
                    return;
                }
            }
            else
            {
                // Om användaren anger något annat än "yes" eller "no", returnera ett felmeddelande
                Console.WriteLine("Invalid input.");
                return;
            }
            Console.WriteLine(user.UserId);
            // Skapa en ny supportticket och sätt användarens ID till ticket.UserID
            var ticket = new TicketModel { UserId = user.UserId };


            Console.Write("Describe your issue: ");
            ticket.Description = Console.ReadLine();


            ticket.Status = TicketStatus.EjPåbörjad;


            ticket.DateCreated = DateTime.Now;

            // Rensa konsolfönstret och meddela användaren att supportticketen har skapats
            Console.Clear();
            Console.WriteLine("Ticket has been created!");

            // Spara supportticketen till databasen med hjälp av databaseService
            await databaseService.SaveTicketToDatabaseAsync(ticket);
        }

        public async Task ShowAllTicketsAsync()
        {
            var databaseService = new DatabaseService();
            var userService = new UserService();

            var tickets = await databaseService.GetAllTicketsAsync();

            Console.Clear();
            foreach (var ticket in tickets)
            {
                var userEmail = await userService.GetUserByIdAsync(ticket.UserId);

                Console.WriteLine($"Ticket ID: {ticket.TicketNumber}");
                Console.WriteLine($"Skapad av Användare: {userEmail.Email} ");
                Console.WriteLine($"Description: {ticket.Description}");
                Console.WriteLine(" ");
            }
        }
        //Söker ut ett specifikt ärende utifrån TicketNumber
        public async Task ShowSpecificTicketAsync()
        {

            var databaseService = new DatabaseService();
            Console.Clear();
            Console.Write("Enter the ticket number you wish to find: ");
            if (int.TryParse(Console.ReadLine(), out int ticketNumber))
            {
                var ticket = await databaseService.GetAsync(ticketNumber);

                if (ticket != null)
                {
                    Console.WriteLine($"TicketNumber: {ticket.TicketNumber}");
                    Console.WriteLine($"Created by User: {ticket.UserId}");
                    Console.WriteLine($"DateCreated: {ticket.DateCreated}");
                    Console.WriteLine($"Status:{ticket.Status}");
                    Console.WriteLine($"Description: {ticket.Description}");
                    Console.WriteLine("");
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine($"No matching ticket with number: {ticketNumber} found.");
                    Console.WriteLine("");
                }
            }
            else
            {
                Console.WriteLine($"Please type a valid ticket number.");
                Console.WriteLine("");
            }
        }
        //Uppdaterar Statusen på ett specifikt ärende
        public async Task UpdateStatusAsync()
        {
            var databaseService = new DatabaseService();
            Console.Clear();
            Console.Write("Write the ticket number you wish to Update: ");
            if (!int.TryParse(Console.ReadLine(), out int ticketNumber))
            {
                Console.WriteLine("Invalid input. Please enter a valid ticket number.");
                return;
            }

            var ticket = await databaseService.GetAsync(ticketNumber);

            if (ticket == null)
            {
                Console.WriteLine($"Ticket {ticketNumber} not found.");
                return;
            }

            Console.Write($"Current status of ticket {ticketNumber}: {ticket.Status}\n");
            Console.Write($"Enter the new status number(1-3) for ticket {ticketNumber} ({string.Join(", ", Enum.GetNames(typeof(TicketStatus)))}): ");

            if (!int.TryParse(Console.ReadLine(), out int newStatusInt))
            {
                Console.WriteLine("Invalid input. Please enter a valid status number.");
                return;
            }

            if (!Enum.IsDefined(typeof(TicketStatus), newStatusInt))
            {
                Console.WriteLine("Invalid input. Please enter a valid status number.");
                return;
            }

            var newStatus = (TicketStatus)newStatusInt;
            ticket.Status = (int)newStatus;

            try
            {
                await databaseService.UpdateAsync(ticket);
                Console.WriteLine($"Status of ticket {ticketNumber} has been updated to {newStatus}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while updating the ticket status: {ex.Message}");
            }
        }
    }
}

