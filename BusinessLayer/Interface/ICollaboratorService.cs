using ModelLayer;
using System;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface ICollaboratorService
    {
        Task<Collaborator> AddCollaborator(string email, int userId, Collaborator collaborator);
    }
}
