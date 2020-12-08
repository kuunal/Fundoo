using AutoMapper;
using BusinessLayer.Interface;
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

        public LabelService(ILabelRepository repository
            , IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<LabelResponseDto> AddLabelAsync(int userId, LabelRequestDto label)
        {
            Label labelModel = _mapper.Map<Label>(label);
            return _mapper.Map<LabelResponseDto>(await _repository.AddLabel(labelModel, userId));
        }

        public async Task<List<LabelResponseDto>> GetLabelAsync(int userId)
        {
            return (await _repository.GetLabelsAsync(userId))
                .Select(label=>_mapper.Map<LabelResponseDto>(label)).ToList();
        }

        public async Task<LabelResponseDto> RemoveLabelAsync(int userId, int labelId)
        {
            return _mapper.Map<LabelResponseDto>(await _repository.RemoveLabelAsync(userId, labelId));
        }
    }
}
