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

        public async Task<Note> DeleteNote(int noteId, int userId)
        {
            Note note = await _context.Notes.FirstOrDefaultAsync(note => note.AccountId == userId && note.NoteId == noteId);
            var deletedNote = _context.Notes.Remove(note);
            await _context.SaveChangesAsync();
            return deletedNote.Entity;
        }

        public async Task<Note> GetNote(int noteId, int userId)
        {
            return await _context.Notes
                        .OrderByDescending(note=>note.IsArchieved)
                        .FirstOrDefaultAsync(note=>note.NoteId == noteId && note.AccountId == userId);
        }

        public async Task<Note> GetNoteByAccountAndCollaborator(int userId, int collaboratorNoteId)
        {
            return await _context.Notes
                .FirstOrDefaultAsync(note => note.AccountId == userId
                                            && note.NoteId == collaboratorNoteId);
        }

        public async Task<List<Note>> GetNotes(int userId)
        {
            return await _context.Notes
                         .Where(note => note.Account.AccountId == userId).ToListAsync();
        }

        public async Task<Note> GetOwnerOfLabel(int labelNoteId, int userId)
        {
            return await _context.Notes
                                .FirstOrDefaultAsync(note => note.NoteId == labelNoteId
                                                            && note.AccountId == userId);
        }

        public async Task<Note> UpdateNote(Note noteToUpdate, Note note)
        {
            return await Task.Run(async () =>
            {
                noteToUpdate.Image = note.Image;
                noteToUpdate.Description = note.Description;
                noteToUpdate.Color = note.Color;
                noteToUpdate.IsArchieved = note.IsArchieved;
                noteToUpdate.IsPin = note.IsPin;
                noteToUpdate.Remainder = note.Remainder;
                noteToUpdate.Title = note.Title;
                await _context.SaveChangesAsync();
                return note;
            });
        }

        public async Task<Note> GetNoteByNoteIdAndUserId(int noteId, int userId)
        {
            return await _context.Notes.FirstOrDefaultAsync(note=>note.NoteId == noteId && note.AccountId == userId);

        }
    }
}
