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
        Task<Note> DeleteNote(int id);
        Task<Note> GetNote(int id);
        Task<List<Note>> GetNotes(int userId);
        Task<Note> UpdateNote(int id, Note note);

    }
}
