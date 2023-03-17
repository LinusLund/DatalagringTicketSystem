using DatalagringTicketSystem.Models.Entities;
using System.ComponentModel;

namespace DatalagringTicketSystem.Models
{
    internal class TicketModel
    {
        public int TicketNumber { get; set; }
        public string Description { get; set; } = null!;
        public DateTime DateCreated { get; set; }
        
        [DefaultValue(TicketStatus.Ej_Påbörjad)]
        public TicketStatus Status { get; set; }

        public Guid UserId { get; set; }
        //Har med UserEmail här för att kunna printa den istället för ID där jag vill.
        public string UserEmail { get; set; } = null!;

        public ICollection<CommentEntity> Comments { get; set; } = new List<CommentEntity>();

    }
}
