using AutoMapper;
using BusinessLayer.ImagesCloud;
using BusinessLayer.Interface;
using ModelLayer;
using ModelLayer.DTOs.NoteDTO;
using RepositoryLayer.Concrete;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class NotesService : INotesService
    {
        private readonly INotesRepository _repository;
        private readonly IMapper _mapper;
        private readonly ICloudService _cloudService;

        public NotesService(INotesRepository repository, IMapper mapper, ICloudService cloudService)
        {
            _repository = repository;
            _mapper = mapper;
            _cloudService = cloudService;
        }
        public async Task<NoteResponseDto> AddNote(NoteRequestDto note, int userid, string email)
        {
            Note noteModel = _mapper.Map<Note>(note);
            noteModel.AccountId = userid;
            if (noteModel.Image.Length > 0) {
                noteModel.Image = await _cloudService.UpdloadToCloud(note.Image, email);
            }
            return _mapper.Map<NoteResponseDto>(await _repository.AddNote(noteModel));
        }

        public async Task<NoteResponseDto> DeleteNote(int noteId, int userId)
        {
            return _mapper.Map<NoteResponseDto>(await _repository.DeleteNote(noteId, userId));
        }

        public async Task<NoteResponseDto> GetNote(int noteId, int userId)
        {
            return _mapper.Map<NoteResponseDto>(await _repository.GetNote(noteId, userId));
        }

        public async Task<List<NoteResponseDto>> GetNotes(int userid)
        {
            return (await _repository.GetNotes(userid))
                .Select(note=> _mapper.Map<NoteResponseDto>(note))
                .ToList();
        }

        public async Task<NoteResponseDto> UpdateNote(int userId, int noteId, NoteRequestDto noteToUpdate)
        {
            Note note = await _repository.GetNoteByNoteIdAndUserId(noteId, userId);
            return _mapper.Map<NoteResponseDto>(await _repository.UpdateNote(_mapper.Map<Note>(noteToUpdate), note));
        }
    }
}
