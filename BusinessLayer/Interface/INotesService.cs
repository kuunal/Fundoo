using ModelLayer;
using ModelLayer.DTOs.NoteDTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface INotesService
    {
        Task<NoteResponseDto> AddNote(NoteRequestDto note, int userid, string email);
        Task<NoteResponseDto> DeleteNote(int noteId, int userid);
        Task<NoteResponseDto> GetNote(int id, int userId);
        Task<List<NoteResponseDto>> GetNotes(int userid);
        Task<NoteResponseDto> UpdateNote(int userId, int noteId, NoteRequestDto note);
    }
}
