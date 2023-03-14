
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DatalagringTicketSystem.Models.Entities
{
    internal class TicketEntity
    {
        [Key]
        public int TicketNumber { get; set; }

        [StringLength(250)]
        [Required]
        public string Description { get; set; } = null!;

        
        public DateTime DateCreated{ get; set; }



        //FK
        public int Status { get; set; }

        [ForeignKey("User")]
        public Guid Id { get; set; }
        public UserEntity User { get; set; } = null!;

        public static implicit operator TicketModel(TicketEntity ticketentity)
        {
            return new TicketModel
            {
                TicketNumber = ticketentity.TicketNumber,
                Description = ticketentity.Description,
                DateCreated = ticketentity.DateCreated,
                Status = (TicketStatus)ticketentity.Status,
                Id = ticketentity.Id

            };

        }

        public static implicit operator TicketEntity(TicketModel ticketentity)
        {
            return new TicketEntity
            {
                TicketNumber = ticketentity.TicketNumber,
                Description = ticketentity.Description,
                DateCreated = ticketentity.DateCreated,
                Status = (int)ticketentity.Status,
                Id = ticketentity.Id

            };

        }
    }
}
