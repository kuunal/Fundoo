using AutoMapper;
using BusinessLayer.Interface;
using CustomException;
using ModelLayer;
using ModelLayer.DTOs.AccountDto;
using RepositoryLayer.Interface;
using System.Threading.Tasks;
using TokenAuthentication;

namespace BusinessLayer.Concrete
{
    public class AccountService : IAccountService
    {
        private readonly ITokenManager _tokenManager; 
        private IAccountRepository _repository;
        private readonly IMapper _mapper;

        public AccountService(IAccountRepository repository, ITokenManager _tokenManager, IMapper mapper)
        {
            _repository = repository;
            this._tokenManager = _tokenManager;
            _mapper = mapper;
        }

        public async Task<AccountResponseDto> Get(int id)
        {
            return _mapper.Map<AccountResponseDto>(await _repository.Get(id));
        }
        

        public async Task<AccountResponseDto> AddAccount(AccountRequestDto account)
        {
            Account user = await _repository.Get(account.Email);
            if (user != null)
            {
                return null;
            }
            Account encryptedPasswordAccount = new Account
            {
                DateOfBirth = account.DateOfBirth,
                Email = account.Email.ToLower(),
                FirstName = account.FirstName,
                LastName = account.LastName,
                Password = BCrypt.Net.BCrypt.HashPassword(account.Password),
                PhoneNumber = account.PhoneNumber
            };
            return _mapper.Map<AccountResponseDto>(await _repository.AddAccount(encryptedPasswordAccount));
        }

        public async Task<(AccountResponseDto, string)> Authenticate(string email, string password)
        {
            Account user = await _repository.Get(email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                throw new FundooException("Login Failed");
            }
            string token = _tokenManager.Encode(user);

            return (_mapper.Map<AccountResponseDto>(user), token);
        }
    }
}
