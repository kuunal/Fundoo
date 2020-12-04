using ModelLayer;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Concrete
{
    class NotesRepository : INotesRepository
    {
        public Task<Note> AddNote()
        {
            throw new NotImplementedException();
        }

        public Task<Note> DeleteNote()
        {
            throw new NotImplementedException();
        }

        public Task<Note> GetNote(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Note> GetNote()
        {
            throw new NotImplementedException();
        }

        public Task<Note> UpdateNote()
        {
            throw new NotImplementedException();
        }
    }
}
