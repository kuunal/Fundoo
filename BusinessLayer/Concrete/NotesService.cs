using BusinessLayer.Interface;
using ModelLayer;
using RepositoryLayer.Concrete;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class NotesService : INotesService
    {
        private readonly INotesRepository _repository;
        public NotesService(INotesRepository repository)
        {
            _repository = repository;
        }
        public async Task<Note> AddNote(Note note, int userid)
        {
            note.AccountId = userid;
            return await _repository.AddNote(note);
        }

        public async Task<Note> DeleteNote(int noteId, int userId)
        {
            return await _repository.DeleteNote(noteId, userId);
        }

        public async Task<Note> GetNote(int noteId, int userId)
        {
            return await _repository.GetNote(noteId, userId);
        }

        public async Task<List<Note>> GetNotes(int userid)
        {
            return await _repository.GetNotes(userid);
        }

        public async Task<Note> UpdateNote(int userId, int noteId, Note note)
        {
            return await _repository.UpdateNote(userId, noteId, note);
        }
    }
}
