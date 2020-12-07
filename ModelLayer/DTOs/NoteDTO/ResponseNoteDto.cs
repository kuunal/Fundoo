using System;
using System.Collections.Generic;
using System.Text;

namespace ModelLayer.DTOs.NoteDTO
{
    public class ResponseNoteDto
    {
        public int NoteId { get; set; }

        public string Title { get; set; }

        public string Color { get; set; }

        public DateTime? Remainder { get; set; }

        public bool IsArchieved { get; set; } = false;


        public bool IsPin { get; set; } = false;

        public string Description { get; set; }
        public string Image { get; set; }

        public int AccountId { get; set; }

    }
}
