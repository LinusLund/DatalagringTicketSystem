using DatalagringTicketSystem.Services;

var menu = new MenuService();

while (true)
{
    Console.Clear();
    Console.WriteLine("1. Create new Ticket");
    Console.WriteLine("2. Show all Tickets");
    Console.WriteLine("3. Show specific Ticket");
    Console.WriteLine("4. Update Ticket Status");
    Console.WriteLine("5. Delete Ticket");
    Console.WriteLine("Choose one option from above(1-5): ");

    switch (Console.ReadLine())
    {
        case "1":
            await menu.CreateNewTicketAsync();
            break;

        case "2":
            await menu.ShowAllTicketsAsync();
            break;

        case "3":
            await menu.CreateNewTicketAsync();
            break;

        case "4":
            Console.Clear();
            await menu.CreateNewTicketAsync();
            break;

        case "5":
            Console.Clear();
            await menu.CreateNewTicketAsync();
            break;
    }
    Console.WriteLine("\nPress any button to continue...");
    Console.ReadKey();
}