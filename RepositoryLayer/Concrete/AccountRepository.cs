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
    public class AccountRepository : IAccountRepository
    {
        private FundooDbContext _context;

        public AccountRepository(FundooDbContext context)
        {
            _context = context;
        }

        public async Task<Account> AddAccount(Account account)
        {
            var result = await _context.Accounts.AddAsync(account);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Account> Get(string email)
        {
            return await _context.Accounts.FirstOrDefaultAsync(account=>account.Email == email);

        }

        public async Task<Account> Get(int id)
        {
            return await _context.Accounts.FirstOrDefaultAsync(account => account.AccountId== id);
        }
    }
}
