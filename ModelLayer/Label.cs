using System.ComponentModel.DataAnnotations;

namespace ModelLayer
{
    public class Label
    {
        [Key]
        public int LabelId { get; set; }

        public int NoteId { get; set; }

        public string Labels { get; set; }
    }
}