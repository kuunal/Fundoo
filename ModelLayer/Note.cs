using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ModelLayer
{
    public class Note
    {
        [Key]
        public int NoteId { get; set; }

        public string Title { get; set; }

        [MinLength(4)]
        [MaxLength(7)]
        public string Color { get; set; }

        public DateTime Remainder { get; set; }

        [Column(TypeName = "BIT")]
        public bool IsArchieved { get; set; }


        [Column(TypeName = "BIT")]
        public bool IsPin { get; set; }

        public string Description { get; set; }
        public string Image { get; set; }

        public int? AccountId { get; set; }

        public Account Owner { get; set; }

        public virtual ICollection<Collaborator> Collaborators { get; set; }

        public virtual ICollection<Label> Labels { get; set; }

    }
}
