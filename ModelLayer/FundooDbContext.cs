using Microsoft.EntityFrameworkCore;

namespace ModelLayer
{
    public class FundooDbContext : DbContext
    {
        public FundooDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Account> Accounts { get; set; }
    }
}
