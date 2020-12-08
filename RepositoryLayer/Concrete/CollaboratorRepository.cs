using CustomException;
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
    public class CollaboratorRepository : ICollaboratorRepository
    {
        private readonly FundooDbContext _context;

        public CollaboratorRepository(FundooDbContext context)
        {
            _context = context;
        }


        public async Task<Collaborator> AddCollaborator(string email, Collaborator collaborator)
        {
            try { 
                Collaborator collaboratorWithSameUser = await _context.Collaborators
                                                .FirstOrDefaultAsync(collaborator=>collaborator.email == email);
                if (collaboratorWithSameUser != null)
                {
                    throw new FundooException("Already added as collaborator");
                }

                var result = await _context.Collaborators.AddAsync(collaborator);
                await _context.SaveChangesAsync();
                return result.Entity;
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException)
            {
                throw new FundooException("No such user or note exists!");
            }
        }

        public async Task<Collaborator> RemoveCollaborator(int collaboratorId, int userId)
        {
            Collaborator isCollaborator = await _context.Collaborators.FindAsync(collaboratorId);
            Note noteOwner = await _context.Notes
                .FirstOrDefaultAsync(note=> note.AccountId == userId 
                                            && note.NoteId == isCollaborator.NoteId);
            if (noteOwner == null)
            {
                throw new FundooException("No such note");
            }
            if (isCollaborator == null)
            {
                throw new FundooException("No such collaborator");
            }
            return await Task.Run(async () =>
            {

                var result = _context.Collaborators.Remove(isCollaborator);
                await _context.SaveChangesAsync();
                return result.Entity;
            });
            
        }

        public async Task<List<Collaborator>> GetCollaboratorAsync(int userId)
        {
            return await _context.Collaborators
                .FromSqlRaw("select Id, email, Noteid  from collaborators where NoteId in (select NoteId from Notes where AccountId = {0})", userId)
                .ToListAsync();
        }
    }
}
