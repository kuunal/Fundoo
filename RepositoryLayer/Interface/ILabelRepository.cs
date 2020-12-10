using ModelLayer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface ILabelRepository
    {
        Task<Label> AddLabel(Label label, int userId);
        Task<List<Label>> GetLabelsAsync(int userId);
        Task<Label> RemoveLabelAsync(Label label);
        Task<Label> GetLabelByIdAsync(int labelId);
    }
}
