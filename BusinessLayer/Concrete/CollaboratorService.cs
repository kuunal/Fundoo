using AutoMapper;
using BusinessLayer.Interface;
using BusinessLayer.MSMQ;
using CustomException;
using EmailService;
using ModelLayer;
using ModelLayer.DTOs.CollaboratorDTO;
using Newtonsoft.Json;
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
        private readonly IMqServices _mqServices;

        public CollaboratorService(INotesRepository noteRepository
            , ICollaboratorRepository collaboratorRepository
            , IMapper mapper
            , IMqServices mqServices) 
        {
            _noteRepository = noteRepository;
            _collaboratorRepository = collaboratorRepository;
            _mapper = mapper;
            _mqServices = mqServices;
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
                Collaborator collaboratorWithSameUser = await _collaboratorRepository.GetCollaboratorByEmail(modelCollaborator.email, modelCollaborator.NoteId);
                if (collaboratorWithSameUser != null)
                {
                    throw new FundooException("Already added as collaborator");
                }
                Note note = await _noteRepository.GetNote(modelCollaborator.NoteId, userId);
                if (note == null)
                {
                    throw new FundooException("No such note!");
                }
                Message message = new EmailService.Message(new string[] { modelCollaborator.email },
                "Added as collaborator",
                $"<h2>You have been added as collaborated by <p style='color:red'>"+email +"</p> To note <p style='color:green'>"+note.Title+"</p><h2>");
                _mqServices.AddToQueue(message);
                return _mapper.Map<CollaboratorResponseDto>(await _collaboratorRepository.AddCollaborator(email, modelCollaborator));
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException)
            {
                throw new FundooException("No such exists!");
            }
        }

        public async Task<CollaboratorResponseDto> RemoveCollaborator(int collaboratorId, int userId)
        {
            Collaborator isCollaborator = await _collaboratorRepository.GetCollaboratorByIdAsync(collaboratorId);
            if (isCollaborator == null)
            {
                throw new FundooException("No such collaborator");
            }
            Note noteOwner = await _noteRepository.GetNoteByAccountAndCollaborator(userId, isCollaborator.NoteId);  
            if (noteOwner == null)
            {
                throw new FundooException("No such note");
            }

            return _mapper.Map<CollaboratorResponseDto>(await _collaboratorRepository.RemoveCollaborator(isCollaborator));
        }

        public async Task<List<CollaboratorResponseDto>> GetCollaborators(int userId)
        {
            return (await _collaboratorRepository.GetCollaboratorAsync(userId))
                .Select(collaborator=>_mapper.Map<CollaboratorResponseDto>(collaborator)).ToList();
        }
    }
}
