using AutoMapper;
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

        public NotesService(INotesRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<NoteResponseDto> AddNote(NoteRequestDto note, int userid)
        {
            Note noteModel = _mapper.Map<Note>(note);
            noteModel.AccountId = userid;
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
