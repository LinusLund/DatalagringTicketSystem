
using System.ComponentModel.DataAnnotations;
using static DatalagringTicketSystem.Models.Entities.TicketStatusEntity;

namespace DatalagringTicketSystem.Models.Entities
{
    internal class TicketEntity
    {
        [Key]
        public int TicketNumber { get; set; }

        [StringLength(250)]
        [Required]
        public string Description { get; set; } = null!;

        [StringLength(100)]
        public DateTime DateCreated{ get; set; } = DateTime.Now;

      

        //FK
        public TicketStatus Status { get; set; }

        public Guid UserId { get; set; }
        public UserEntity User { get; set; } = null!;

        public static implicit operator TicketModel(TicketEntity ticketentity)
        {
            return new TicketModel
            {
                TicketNumber = ticketentity.TicketNumber,
                Description = ticketentity.Description,
                DateCreated = ticketentity.DateCreated,
                Status = (TicketStatus)ticketentity.Status,
                UserEmail = ticketentity.User.Email

            };

        }

        public static implicit operator TicketEntity(TicketModel ticketentity)
        {
            return new TicketEntity
            {
                TicketNumber = ticketentity.TicketNumber,
                Description = ticketentity.Description,
                DateCreated = ticketentity.DateCreated   

            };

        }
    }
}
