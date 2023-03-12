using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatalagringTicketSystem.Models.Entities
{
    internal class CommentEntity
    {
        public int CommentId { get; set; }

        public string CommentText { get; set; } = null!;
    }
}
