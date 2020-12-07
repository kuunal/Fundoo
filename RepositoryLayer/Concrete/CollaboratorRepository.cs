using Microsoft.EntityFrameworkCore;
using ModelLayer;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
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
            Collaborator collaboratorWithSameUser = await _context.Collaborators
                                            .FirstOrDefaultAsync(collaborator=>collaborator.email == email);
            if (collaboratorWithSameUser != null)
            {
                return null;
            }

            var result = await _context.Collaborators.AddAsync(collaborator);
            await _context.SaveChangesAsync();
            return result.Entity;
        }
    }
}
