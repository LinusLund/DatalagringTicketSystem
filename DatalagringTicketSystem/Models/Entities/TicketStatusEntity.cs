using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatalagringTicketSystem.Models.Entities
{
    
    
        public enum TicketStatus
        {
            EjPåbörjad = 1,
            Påbörjad = 2,
            Slutförd = 3
        }

        internal class TicketStatusEntity
        {
            [Key]
            public int Id { get; set; }
            public TicketStatus Status { get; set; }
        }
    

}
