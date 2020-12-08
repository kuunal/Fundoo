using System;
using System.Collections.Generic;
using System.Text;

namespace ModelLayer.DTOs.CollaboratorDTO
{
    public class CollaboratorResponseDto 
    {
        public int Id { get; set; }
        public string email { get; set; }
        public int NoteId { get; set; }
    }
}
