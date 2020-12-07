using BusinessLayer.Interface;
using ModelLayer;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class CollaboratorService : ICollaboratorService
    {
        private readonly INotesRepository _noteRepository;
        private readonly ICollaboratorRepository _collaboratorRepository;


        public CollaboratorService(INotesRepository noteRepository, ICollaboratorRepository collaboratorRepository) 
        {
            _noteRepository = noteRepository;
            _collaboratorRepository = collaboratorRepository;
        }
        public async Task<Collaborator> AddCollaborator(string email, int userId, Collaborator collaborator)
        {
            Note note = await _noteRepository.GetNote(collaborator.NoteId, userId);
            if (note == null)
            {
                return null;
            }
            return await _collaboratorRepository.AddCollaborator(email, collaborator);
        }
    }
}
