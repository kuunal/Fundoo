using AutoMapper;
using BusinessLayer.Interface;
using CustomException;
using ModelLayer;
using ModelLayer.DTOs.LabelDTO;
using RepositoryLayer.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class LabelService : ILabelService
    {

        private readonly ILabelRepository _repository;
        private readonly IMapper _mapper;
        private readonly INotesRepository _notesRepository;

        public LabelService(ILabelRepository repository
            , IMapper mapper 
            , INotesRepository notesRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _notesRepository = notesRepository;
        }

        public async Task<LabelResponseDto> AddLabelAsync(int userId, LabelRequestDto label)
        {
            Label labelModel = _mapper.Map<Label>(label);
            Note isOwner = await _notesRepository.GetOwnerOfLabel(label.NoteId, userId);
            if (isOwner == null)
            {
                throw new FundooException("No such note exists!");
            }
            return _mapper.Map<LabelResponseDto>(await _repository.AddLabel(labelModel, userId));
        }

        public async Task<List<LabelResponseDto>> GetLabelAsync(int userId)
        {
            return (await _repository.GetLabelsAsync(userId))
                .Select(label=>_mapper.Map<LabelResponseDto>(label)).ToList();
        }

        public async Task<LabelResponseDto> RemoveLabelAsync(int userId, int labelId)
        {
            Label label = await _repository.GetLabelByIdAsync(labelId);
            if (label == null)
            {
                throw new FundooException("No such label exist!");
            }
            Note isOwner = await _notesRepository.GetOwnerOfLabel(label.NoteId, userId);
            if (isOwner == null)
            {
                throw new FundooException("No such note exists!");
            }
            return _mapper.Map<LabelResponseDto>(await _repository.RemoveLabelAsync(label));
        }
    }
}
