﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ModelLayer.DTOs.LabelDTO
{
    public class LabelResponseDto
    {
        public int LabelId { get; set; }
        public int NoteId { get; set; }
        public string Labels { get; set; }
    }
}
