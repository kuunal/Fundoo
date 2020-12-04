using Microsoft.EntityFrameworkCore;

namespace ModelLayer
{
    public class FundooDbContext : DbContext
    {
        public FundooDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Collaborator> Collaborators { get; set; }
        public DbSet<Label> Labels { get; set; }

    }
}
