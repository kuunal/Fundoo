using AutoMapper;
using BusinessLayer.Interface;
using ModelLayer;
using ModelLayer.DTOs.CollaboratorDTO;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class CollaboratorService : ICollaboratorService
    {
        private readonly INotesRepository _noteRepository;
        private readonly ICollaboratorRepository _collaboratorRepository;
        private readonly IMapper _mapper;

        public CollaboratorService(INotesRepository noteRepository
            , ICollaboratorRepository collaboratorRepository
            , IMapper mapper) 
        {
            _noteRepository = noteRepository;
            _collaboratorRepository = collaboratorRepository;
            _mapper = mapper;
        }
        public async Task<CollaboratorResponseDto> AddCollaborator(string email, int userId, CollaboratorRequestDto collaborator)
        {
            try
            {
                Collaborator modelCollaborator = _mapper.Map<Collaborator>(collaborator);
                if (email == modelCollaborator.email)
                {
                    throw new Exception("Cannot collaborate with self");
                }
                Note note = await _noteRepository.GetNote(modelCollaborator.NoteId, userId);
                if (note == null)
                {
                    return null;
                }
                return _mapper.Map<CollaboratorResponseDto>(await _collaboratorRepository.AddCollaborator(email, modelCollaborator));
            }catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<CollaboratorResponseDto> RemoveCollaborator(int collaboratorId, int userId)
        {
            return _mapper.Map<CollaboratorResponseDto>(await _collaboratorRepository.RemoveCollaborator(collaboratorId, userId));
        }

        public async Task<List<CollaboratorResponseDto>> GetCollaborators(int userId)
        {
            return (await _collaboratorRepository.GetCollaboratorAsync(userId))
                .Select(collaborator=>_mapper.Map<CollaboratorResponseDto>(collaborator)).ToList();
        }
    }
}
