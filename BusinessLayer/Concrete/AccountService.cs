using AutoMapper;
using BusinessLayer.Interface;
using CustomException;
using EmailService;
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
        private readonly IEmailSender _emailSender;

        public AccountService(IAccountRepository repository
            , ITokenManager _tokenManager
            , IMapper mapper
            , IEmailSender emailSender)
        {
            _repository = repository;
            this._tokenManager = _tokenManager;
            _mapper = mapper;
            _emailSender = emailSender;
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

        public async Task ForgotPassword(string email, string currentUrl)
        {
            Account user = await _repository.Get(email);
            if (user == null)
            {
                throw new FundooException("No such user", 404);
            }
            string jwt = _tokenManager.Encode(user);
            string url = "https://" + currentUrl + "/html/reset.html?" + jwt;
            Message message = new Message(new string[] { user.Email },
                    "Password Reset Email",
                    $"<h6>Click on the link to reset password<h6><a href='{url}'>{jwt}</a>");
            await _emailSender.SendEmail(message);
        }

    }
}
