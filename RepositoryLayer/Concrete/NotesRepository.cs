using Microsoft.EntityFrameworkCore;
using ModelLayer;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Concrete
{
    public class NotesRepository : INotesRepository
    {
        private readonly FundooDbContext _context;
        public NotesRepository(FundooDbContext context) 
        {
            _context = context;
        }

        public async Task<Note> AddNote(Note note)
        {
            var result = await _context.Notes.AddAsync(note);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Note> DeleteNote(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Note> GetNote(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Note>> GetNotes(int userId)
        {
            return await _context.Notes
                         .Where(note => note.Owner.AccountId == userId).ToListAsync();
        }

        public Task<Note> UpdateNote(int id, Note note)
        {
            throw new NotImplementedException();
        }
    }
}
