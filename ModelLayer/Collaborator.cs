using System.ComponentModel.DataAnnotations;

namespace ModelLayer
{
    public class Collaborator
    {
        [Key]
        public int Id { get; set; }
            
        public Account AccountId { get; set; }

        public Note NoteId { get; set; }
    }
}