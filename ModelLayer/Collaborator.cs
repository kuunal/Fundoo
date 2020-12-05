using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelLayer
{
    public class Collaborator
    {
        [Key]
        public int Id { get; set; }

        public int AccountId { get; set; }

        public int NoteId { get; set; }

        public virtual Account Account { get; set; }

        public virtual Note Note { get; set; }
    }
}