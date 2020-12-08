using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ModelLayer.DTOs.NoteDTO
{
    public class NoteRequestDto
    {
        public string Title { get; set; }

        [MinLength(4)]
        [MaxLength(7)]
        public string Color { get; set; }

        public DateTime? Remainder { get; set; }

        public bool IsArchieved { get; set; } = false;


        public bool IsPin { get; set; } = false;

        public string Description { get; set; }
        public string Image { get; set; }

    }
}
