using BusinessLayer.Interface;
using ModelLayer;
using RepositoryLayer.Concrete;
using RepositoryLayer.Interface;
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

        public Task<Note> DeleteNote(int id)
        {
            return await _repository.DeleteNote();
        }

        public Task<Note> GetNote(int id)
        {
            return await _repository.GetNote(id);
        }

        public Task<Note> GetNote()
        {
            return await _repository.GetNote();
        }

        public Task<Note> UpdateNote(int id, Note note)
        {
            await _repository.UpdateNote(int id, Note note);
        }
    }
}
