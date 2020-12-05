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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>().HasMany(acc => acc.Collaborators)
                .WithOne(collaborator => collaborator.Account)
                .HasForeignKey(Collaborator => Collaborator.AccountId);


            modelBuilder.Entity<Note>().HasMany(note => note.Collaborators)
                .WithOne(collaborator => collaborator.Note)
                .HasForeignKey(Collaborator => Collaborator.NoteId);


            modelBuilder.Entity<Note>().HasOne(n => n.Owner)
                .WithOne(a => a.Note)
                .HasForeignKey<Note>(n => n.AccountId);

            modelBuilder.Entity<Collaborator>().HasIndex(fields => new { fields.AccountId, fields.NoteId }).IsUnique();
        }
    }
}
