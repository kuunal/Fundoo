using ModelLayer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface INotesService
    {
        Task<Note> AddNote(Note note, int userid);
        Task<Note> DeleteNote(int noteId, int userid);
        Task<Note> GetNote(int id, int userId);
        Task<List<Note>> GetNotes(int userid);
        Task<Note> UpdateNote(int userId, int noteId, Note note);
    }
}
