using ModelLayer;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface ILabelService
    {
        Task<Label> AddLabelAsync(int userId, Label label);

        Task<List<Label>> GetLabelAsync(int userId);
    }
}
