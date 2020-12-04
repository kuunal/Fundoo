using ModelLayer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    interface INotesRepository
    {
        Task<Note> AddNote();
        Task<Note> DeleteNote();
        Task<Note> GetNote(int id);
        Task<Note> GetNote();
        Task<Note> UpdateNote();

    }
}
