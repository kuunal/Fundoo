using BusinessLayer.Interface;
using ModelLayer;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class AccountService : IAccountService
    {
        private IAccountRepository _repository;

        public AccountService(IAccountRepository repository)
        {
            _repository = repository;
        }

        public async Task<Account> Get(int id)
        {
            return await _repository.Get(id);
        }
        

        public async Task<Account> AddAccount(Account account)
        {
            Account encryptedPasswordAccount = new Account
            {
                DateOfBirth = account.DateOfBirth,
                Email = account.Email,
                FirstName = account.FirstName,
                LastName = account.LastName,
                Password = BCrypt.Net.BCrypt.HashPassword(account.Password),
                PhoneNumber = account.PhoneNumber
            };
            return await _repository.AddAccount(encryptedPasswordAccount);
        }

        public Task<Account> Authenticate(string email, string password)
        {
            throw new NotImplementedException();
        }
    }
}
