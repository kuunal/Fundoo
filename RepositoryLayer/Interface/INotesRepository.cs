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
        Task<Note> UpdateNote(Note noteToUpdate, Note note);
        Task<Note> GetNoteByAccountAndCollaborator(int userId, int collaboratorNoteId);
        Task<Note> GetOwnerOfLabel(int labelNoteId, int userId);
        Task<Note> GetNoteByNoteIdAndUserId(int noteId, int userId);
    }
}
