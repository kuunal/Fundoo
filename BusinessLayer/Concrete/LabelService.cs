using BusinessLayer.Interface;
using ModelLayer;
using RepositoryLayer.Interface;
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
    }
}
