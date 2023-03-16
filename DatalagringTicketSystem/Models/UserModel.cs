namespace DatalagringTicketSystem.Models
{
    internal class UserModel
    {
        public Guid UserId { get; set; } = new Guid();
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
    }
}
