using BusinessLayer.Interface;
using ModelLayer;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class NoteService : INoteService
    {
        public Task<Note> AddNote()
        {
            throw new System.NotImplementedException();
        }

        public Task<Note> DeleteNote()
        {
            throw new System.NotImplementedException();
        }

        public Task<Note> GetNote(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<Note> GetNote()
        {
            throw new System.NotImplementedException();
        }

        public Task<Note> UpdateNote()
        {
            throw new System.NotImplementedException();
        }
    }
}
