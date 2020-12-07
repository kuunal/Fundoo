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
    }
}
