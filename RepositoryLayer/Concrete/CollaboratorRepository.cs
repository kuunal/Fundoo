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

        public async Task<Collaborator> GetCollaboratorByEmail(string email, int noteId)
        {
            return await _context.Collaborators
                                .FirstOrDefaultAsync(collaborator => collaborator.email == email && collaborator.NoteId == noteId);

        }

        public async Task<Collaborator> AddCollaborator(string email, Collaborator collaborator)
        {
            var result = await _context.Collaborators.AddAsync(collaborator);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Collaborator> RemoveCollaborator(Collaborator collaborator)
        {
            
            
            return await Task.Run(async () =>
            {

                var result = _context.Collaborators.Remove(collaborator);
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

        public async Task<Collaborator> GetCollaboratorByIdAsync(int collaboratorId)
        {
            return await _context.Collaborators.FindAsync(collaboratorId);
        }
    }
}
