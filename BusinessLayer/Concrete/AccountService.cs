using BusinessLayer.Interface;
using Greeting.TokenAuthentication;
using ModelLayer;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TokenAuthentication;

namespace BusinessLayer.Concrete
{
    public class AccountService : IAccountService
    {
        private readonly TokenManager _tokenManager; 
        private IAccountRepository _repository;

        public AccountService(IAccountRepository repository, ITokenManager _tokenManager)
        {
            _repository = repository;
            _tokenManager = _tokenManager;
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

        public async Task<(Account, string)> Authenticate(string email, string password)
        {
            Account user = await _repository.Get(email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                return (null, null);
            }
            string token = _tokenManager.Encode(user);
            return (user, token);
        }
    }
}
