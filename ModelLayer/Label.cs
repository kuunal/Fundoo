using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelLayer
{
    public class Label
    {
        [Key]
        public int LabelId { get; set; }

        [ForeignKey("Note")]
        public int NoteId { get; set; }

        public string Labels { get; set; }

        public Note Note { get; set; }

    }
}