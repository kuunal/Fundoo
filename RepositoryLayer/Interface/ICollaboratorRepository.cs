﻿using ModelLayer;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface ICollaboratorRepository
    {
        Task<Collaborator> AddCollaborator(string email, Collaborator collaborator);
        Task<Collaborator> RemoveCollaborator(Collaborator collaborator);
        Task<List<Collaborator>> GetCollaboratorAsync(int userId);
        Task<Collaborator> GetCollaboratorByEmail(string email, int noteId);
        Task<Collaborator> GetCollaboratorByIdAsync(int collaboratorId);
    }
}
