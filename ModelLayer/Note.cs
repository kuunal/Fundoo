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
        public int IsArchieved { get; set; }


        [Column(TypeName = "BIT")]
        public int IsPin { get; set; }

        public string Description { get; set; }
        public string Image { get; set; }
    }
}
