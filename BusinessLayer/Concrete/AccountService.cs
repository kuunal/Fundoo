using BusinessLayer.Interface;
using ModelLayer;
using RepositoryLayer.Interface;
using System.Threading.Tasks;
using TokenAuthentication;

namespace BusinessLayer.Concrete
{
    public class AccountService : IAccountService
    {
        private readonly ITokenManager _tokenManager; 
        private IAccountRepository _repository;

        public AccountService(IAccountRepository repository, ITokenManager _tokenManager)
        {
            _repository = repository;
            this._tokenManager = _tokenManager;
        }

        public async Task<Account> Get(int id)
        {
            return await _repository.Get(id);
        }
        

        public async Task<Account> AddAccount(Account account)
        {
            Account user = await _repository.Get(account.Email);
            if (user != null)
            {
                return null;
            }
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
