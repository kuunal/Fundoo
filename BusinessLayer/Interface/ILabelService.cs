using ModelLayer;
using ModelLayer.DTOs.LabelDTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface ILabelService
    {
        Task<LabelResponseDto> AddLabelAsync(int userId, LabelRequestDto label);
        Task<List<LabelResponseDto>> GetLabelAsync(int userId);
        Task<LabelResponseDto> RemoveLabelAsync(int userId, int labelId);
    }
}
