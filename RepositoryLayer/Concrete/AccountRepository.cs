﻿using Microsoft.EntityFrameworkCore;
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
        public async Task<List<Account>> Get()
        {
            return await _context.Accounts.ToListAsync();
        }
    }
}