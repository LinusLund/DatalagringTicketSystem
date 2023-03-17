using System.ComponentModel.DataAnnotations;


namespace DatalagringTicketSystem.Models.Entities
{
    
    
        public enum TicketStatus
        {
            Ej_Påbörjad = 1,
            På_börjad = 2,
            Slutförd = 3
        }

        internal class TicketStatusEntity
        {
            [Key]
            public int StatusId { get; set; }
            public TicketStatus Status { get; set; } 
        }
    

}
