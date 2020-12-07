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
                .HasForeignKey(c => c.email)
                .HasPrincipalKey(acc => acc.Email)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<Note>().HasMany(note => note.Collaborators)
                .WithOne(collaborator => collaborator.Note)
                .HasForeignKey(Collaborator => Collaborator.NoteId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Account>().HasMany(n => n.Note)
                .WithOne(a => a.Account)
                .HasForeignKey(Note=>Note.AccountId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Note>()
                .HasMany(note => note.Labels)
                .WithOne(label => label.Note)
                .HasForeignKey(label => label.NoteId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<Collaborator>().HasIndex(fields => new { fields.AccountId, fields.NoteId }).IsUnique();
        }
    }
}
