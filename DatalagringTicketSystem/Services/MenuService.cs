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
           
            var databaseService = new DatabaseService();
            var userService = new UserService();

            // Vi låtasas att jag har byggt in en kontofunktion med inlogg och autentisering och grejer.
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
                // Om användaren har ett konto, så söker vi i databasen efter en matchande Email.
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
            // Skapa en ny supportticket och sätt användarens ID till ticket.UserID.
            // Egentligen nu när jag tänker, så kanske man vill ha en tabell som lånad ID från användare och från Tickets.
            // så kan man söka i den tabellen. istället för att  låna in användar-idt i Ticket-tabellen.
            var ticket = new TicketModel { UserId = user.UserId };


            Console.Write("Describe your issue: ");
            ticket.Description = Console.ReadLine();


            ticket.Status = TicketStatus.Ej_Påbörjad;


            ticket.DateCreated = DateTime.Now;

           
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
        //Söker ut ett specifikt ärende utifrån TicketNumber, visar även kommentarer på det specifika ärendet om det finns några.
        public async Task ShowSpecificTicketAsync()
        {
            var ticketservice = new TicketService();

            var ticket = await ticketservice.GetTicketAsync();
            {

                if (ticket != null)
                {
                    Console.WriteLine($"TicketNumber: {ticket.TicketNumber}");
                    Console.WriteLine($"Created by User: {ticket.UserId}");
                    Console.WriteLine($"DateCreated: {ticket.DateCreated}");
                    Console.WriteLine($"Status:{ticket.Status}");
                    Console.WriteLine($"Description: {ticket.Description}");
                    Console.WriteLine("");

                    //OM det finns kommentarer, rada upp dom i datumföljd.
                    if (ticket.Comments.Any())
                    {
                        Console.WriteLine("Comments:");
                        foreach (var comment in ticket.Comments.OrderBy(c => c.CommentDateTime))
                        {
                            Console.WriteLine($" - {comment.CommentDateTime.ToString()} - {comment.CommentText}");
                        }
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine($"No matching ticket with number: {ticket} found.");
                    Console.WriteLine("");

                }

            }
           
        }
        //Uppdaterar Statusen på ett specifikt ärende
        public async Task UpdateStatusAsync()
        {
            var databaseservice = new DatabaseService();
            var ticketservice = new TicketService();

            
            var ticket = await ticketservice.GetTicketAsync();

            if (ticket == null)
            {
                Console.WriteLine($"Ticket {ticket.TicketNumber} not found.");
                return;
            }

            Console.Write($"Current status of ticket {ticket.TicketNumber}: {ticket.Status}\n");
            Console.Write($"Enter the new status number(1-3) for ticket {ticket.TicketNumber} ({string.Join(", ", Enum.GetNames(typeof(TicketStatus)))}): ");

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
            ticket.Status = (TicketStatus)(int)newStatus;

            try
            {
                await databaseservice.UpdateAsync(ticket);
                Console.WriteLine($"Status of ticket {ticket.TicketNumber} has been updated to {newStatus}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while updating the ticket status: {ex.Message}");
            }
        }

        public async Task WriteCommentAsync()
        {
            var databaseService = new DatabaseService();
            var ticketservice = new TicketService();

            var ticket = await ticketservice.GetTicketAsync();

            if (ticket == null)
            {
                Console.WriteLine($"Ticket {ticket.TicketNumber} not found.");
                return;
            }
            Console.Write("Please enter a comment...");

            string comment = Console.ReadLine() ?? "";

            await databaseService.AddCommentAsync(ticket.TicketNumber, comment);

        }

    }
}

