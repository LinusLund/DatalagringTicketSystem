using DatalagringTicketSystem.Models.Entities;

namespace DatalagringTicketSystem.Models
{
    internal class CommentModel
    {
        public int CommentId { get; set; }
        public string CommentText { get; set; } = null!;
        public DateTime CommentDateTime { get; set; }
        public int TicketId { get; set; }
        public TicketEntity Ticket { get; set; } = null!;
    }
}