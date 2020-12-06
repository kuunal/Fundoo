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
        Task<Note> DeleteNote(int id);
        Task<Note> GetNote(int id, int userid);
        Task<List<Note>> GetNotes(int userid);
        Task<Note> UpdateNote(int id, Note note);
    }
}
