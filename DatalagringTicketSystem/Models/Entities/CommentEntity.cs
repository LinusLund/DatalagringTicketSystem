﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;


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
        public int TicketId { get; set; }
        public TicketEntity Ticket { get; set; } = null!;

    }

}

