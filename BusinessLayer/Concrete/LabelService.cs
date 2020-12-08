using BusinessLayer.Interface;
using ModelLayer;
using RepositoryLayer.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class LabelService : ILabelService
    {

        private readonly ILabelRepository _repository;

        public LabelService(ILabelRepository repository)
        {
            _repository = repository;
        }

        public Task<Label> AddLabelAsync(int userId, Label label)
        {
            return _repository.AddLabel(label, userId);
        }

        public Task<List<Label>> GetLabelAsync(int userId)
        {
            return _repository.GetLabelsAsync(userId);
        }

        public Task<Label> RemoveLabelAsync(int userId, int labelId)
        {
            return _repository.RemoveLabelAsync(userId, labelId);
        }
    }
}
