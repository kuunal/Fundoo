using System;
using System.Collections.Generic;
using System.Text;

namespace ModelLayer.DTOs.NoteDTO
{
    public class NoteResponseDto : NoteRequestDto
    {
        public int NoteId { get; set; }
    }
}
