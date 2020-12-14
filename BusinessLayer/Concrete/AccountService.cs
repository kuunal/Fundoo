using AutoMapper;
using BusinessLayer.Exceptions;
using BusinessLayer.Interface;
using BusinessLayer.MSMQ;
using CustomException;
using EmailService;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ModelLayer;
using ModelLayer.DTOs.AccountDto;
using RepositoryLayer.Interface;
using System;
using System.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TokenAuthentication;

namespace BusinessLayer.Concrete
{
    public class AccountService : IAccountService
    {
        private readonly ITokenManager _tokenManager; 
        private IAccountRepository _repository;
        private readonly IMapper _mapper;
        private readonly IMqServices _mqServices;
        private readonly IConfiguration _configuration;

        public AccountService(IAccountRepository repository
            , ITokenManager _tokenManager
            , IMapper mapper
            , IMqServices mqServices
            , IConfiguration configuration) 
        {
            _repository = repository;
            this._tokenManager = _tokenManager;
            _mapper = mapper;
            _mqServices = mqServices;
            _configuration = configuration;
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

        public async Task<(AccountResponseDto, string, string)> Authenticate(string email, string password)
        {
            Account user = await _repository.Get(email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                throw new FundooException(ExceptionMessages.INVALID_CREDENTIALS, 401);
            }
            string token = _tokenManager.Encode(user);
            byte[] refreshSecretArray = Encoding.ASCII.GetBytes(_configuration.GetSection("Jwt")["RefreshSecretKey"]);
            string refreshToken = _tokenManager.Encode(user
                                            , Convert.ToInt32(_configuration.GetSection("Jwt")["RefreshExpiryTime"])
                                            , refreshSecretArray);
            return (_mapper.Map<AccountResponseDto>(user), token, refreshToken);
        }

        public async Task ForgotPassword(string email, string currentUrl)
        {
            Account user = await _repository.Get(email);
            if (user == null)
            {
                throw new FundooException(ExceptionMessages.NO_SUCH_USER, 404);
            }
            byte[] secretKey = Encoding.ASCII.GetBytes(_configuration.GetSection("Jwt")["ResetPasswordSecretKey"]);
            int expiryTime = Convert.ToInt32(_configuration.GetSection("Jwt")["ExpiryTime"]);
            string jwt = _tokenManager.Encode(user, expiryTime, secretKey);
            string url = "https://" + currentUrl + "/html/reset.html?" + jwt;
            Message message = new Message(new string[] { user.Email },
                    "Password Reset Email",
                    $"<h6>Click on the link to reset password<h6><a href='{url}'>{jwt}</a>");
            _mqServices.AddToQueue(message);
        }

        public async Task<int> ResetPassword(string password, string token)
        {
            ClaimsPrincipal claims = _tokenManager.Decode(token, Encoding.ASCII.GetBytes(_configuration.GetSection("Jwt")["ResetPasswordSecretKey"]));
            var claim = claims.Claims.ToList();
            string email = claim[1].Value;
            Account user = await _repository.Get(email);
            return (await _repository.ResetPassword(user, BCrypt.Net.BCrypt.HashPassword(password)));
        }

        public async Task<string> GetNewTokenAsync(string refreshToken)
        {
            ClaimsPrincipal claim;
            try
            {
                claim = _tokenManager.Decode(refreshToken);
            }
            catch (SecurityTokenExpiredException)
            {
                throw new FundooException(ExceptionMessages.TOKEN_EXPIRED);
            }
            catch(Exception)
            {
                throw new FundooException(ExceptionMessages.INVALID_TOKEN);
            }
            var claimList = claim.Claims.ToList();
            Account account = await _repository.Get(claimList[1].Value);
            return _tokenManager.Encode(account);
        }
    }
}
