using ModelLayer;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface ICollaboratorRepository
    {
        Task<Collaborator> AddCollaborator(Collaborator collaborator);
    }
}
