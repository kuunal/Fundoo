using ModelLayer;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface ICollaboratorRepository
    {
        Task<Collaborator> AddCollaborator(string email, Collaborator collaborator);

        Task<Collaborator> RemoveCollaborator(int collaboratorId, int userId);

        Task<List<Collaborator>> GetCollaboratorAsync(int userId);
    }
}
