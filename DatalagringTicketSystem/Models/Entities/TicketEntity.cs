
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

        public DateTime DateCreated { get; set; }

        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public UserEntity User { get; set; } = null!;

        // New property for TicketStatus
        public int TicketStatusId { get; set; }

        // Navigation property for TicketStatus
        [ForeignKey("TicketStatusId")]
        public TicketStatusEntity TicketStatus { get; set; } = null!;

        //Ett ärende kan ha flera kommentarer, men en kommentar kan bara höra till ett ärende.
        public ICollection<CommentEntity> Comments { get; set; } = new List<CommentEntity>();

        public static implicit operator TicketModel(TicketEntity ticketentity)
        {
            return new TicketModel
            {
                TicketNumber = ticketentity.TicketNumber,
                Description = ticketentity.Description,
                DateCreated = ticketentity.DateCreated,
                UserId = ticketentity.UserId,
                TicketStatus = ticketentity.TicketStatus?.Status ?? TicketStatus.EjPåbörjad,
                Comments = ticketentity.Comments,
            };
        }

        public static implicit operator TicketEntity(TicketModel ticketentity)
        {
            return new TicketEntity
            {
                TicketNumber = ticketentity.TicketNumber,
                Description = ticketentity.Description,
                DateCreated = ticketentity.DateCreated,
                UserId = ticketentity.UserId,
                TicketStatusId = (int)ticketentity.TicketStatus,
                Comments = ticketentity.Comments,
            };
        }
    }

}
