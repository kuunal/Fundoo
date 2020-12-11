using Microsoft.EntityFrameworkCore;
using ModelLayer;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Concrete
{
    public class LabelRepository : ILabelRepository
    {
        private readonly FundooDbContext _context;

        public LabelRepository(FundooDbContext context)
        {
            _context = context;
        }
        public async Task<Label> AddLabel(Label label, int userId)
        {
            
            var result = await _context.Labels.AddAsync(label);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Label> GetLabelByIdAsync(int labelId)
        {
            return await _context.Labels.FindAsync(labelId);
        }

        public async Task<List<Label>> GetLabelsAsync(int userId)
        {

            return await _context.Labels.Join(
                _context.Notes,
                labels => labels.NoteId,
                notes => (notes.AccountId == userId)? notes.NoteId : -1,
                (labels, notes) => new Label
                {
                    LabelId = labels.LabelId,
                    NoteId = labels.NoteId,
                    Labels = labels.Labels
                }).ToListAsync();
        }
        public async Task<Label> RemoveLabelAsync(Label label)
        {   
            return await Task.Run(async () =>
            {

            var result = _context.Labels.Remove(label);
                await _context.SaveChangesAsync();
                return result.Entity;
            });
        }

    }
}
