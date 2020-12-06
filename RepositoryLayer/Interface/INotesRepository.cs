using ModelLayer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface INotesRepository
    {
        Task<Note> AddNote(Note note);
        Task<Note> DeleteNote(int noteId, int userId);
        Task<Note> GetNote(int noteId, int userId);
        Task<List<Note>> GetNotes(int userId);  
        Task<Note> UpdateNote(int userId, int noteid, Note note);

    }
}
