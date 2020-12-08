using ModelLayer;
using ModelLayer.DTOs.CollaboratorDTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface ICollaboratorService
    {
        Task<CollaboratorResponseDto> AddCollaborator(string email, int userId, CollaboratorRequestDto collaborator);

        Task<CollaboratorResponseDto> RemoveCollaborator(int collaboratorId, int userId);

        Task<List<CollaboratorResponseDto>> GetCollaborators(int userId);
    }
}
