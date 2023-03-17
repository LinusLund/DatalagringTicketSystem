using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatalagringTicketSystem.Models.Entities
{
    internal class CommentEntity
    {
        [Key]
        public int CommentId { get; set; }

        [Required]
        [StringLength(250)]
        public string CommentText { get; set; } = null!;

        public DateTime CommentDateTime { get; set; }

        public int TicketNumber { get; set; }
        [ForeignKey("TicketNumber")]
        public TicketEntity Ticket { get; set; } = null!;

        public static implicit operator CommentModel(CommentEntity commententity)
        {
            return new CommentModel
            {
                CommentId= commententity.CommentId,
                CommentText= commententity.CommentText,
                CommentDateTime= commententity.CommentDateTime,
                Ticket= commententity.Ticket,
                   

            };

        }

        public static implicit operator CommentEntity(CommentModel commententity)
        {
            return new CommentEntity
            {
                CommentId = commententity.CommentId,
                CommentText = commententity.CommentText,
                CommentDateTime = commententity.CommentDateTime,
                Ticket = commententity.Ticket
            };
        }

    }

}

