using Microsoft.EntityFrameworkCore;
using ModelLayer;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
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
            Note noteOwner = await _context.Notes.FirstOrDefaultAsync(note => note.AccountId == userId);
            if (noteOwner == null)
            {
                throw new Exception("No such note exists!");
            }
            var result = await _context.Labels.AddAsync(label);
            await _context.SaveChangesAsync();
            return result.Entity;s
        }
    }
}
