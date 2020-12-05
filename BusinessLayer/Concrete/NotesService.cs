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
        public async Task<Note> AddNote(Note note)
        {
            return await _repository.AddNote(note);
        }

        public async Task<Note> DeleteNote(int id)
        {
            return await _repository.DeleteNote(id);
        }

        public async Task<Note> GetNote(int id)
        {
            return await _repository.GetNote(id);
        }

        public async Task<List<Note>> GetNotes()
        {
            int userId = 1;
            return await _repository.GetNotes(userId);
        }

        public async Task<Note> UpdateNote(int id, Note note)
        {
            return await _repository.UpdateNote(id, note);
        }
    }
}
