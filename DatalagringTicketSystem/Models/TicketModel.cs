using DatalagringTicketSystem.Models.Entities;
using System.ComponentModel;

namespace DatalagringTicketSystem.Models
{
    internal class TicketModel
    {
        public int TicketNumber { get; set; }
        public string Description { get; set; } = null!;
        public DateTime DateCreated { get; set; }
        [DefaultValue(Entities.TicketStatus.EjPåbörjad)]
        public TicketStatus Status { get; set; }
        public Guid Id { get; set; }
        public string UserEmail { get; set; } = null!;

     
    }
}
