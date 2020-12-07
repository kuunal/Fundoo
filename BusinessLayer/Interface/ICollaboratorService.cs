using ModelLayer;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface ICollaboratorService
    {
        Task<Collaborator> AddCollaborator(string email, int userId, Collaborator collaborator);

        Task<Collaborator> RemoveCollaborator(int collaboratorId, int userId);

        Task<List<Collaborator>> GetCollaborators(int userId);
    }
}
