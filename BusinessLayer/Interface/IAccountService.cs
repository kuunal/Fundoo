using ModelLayer;
using ModelLayer.DTOs.AccountDto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface IAccountService
    {
        Task<AccountResponseDto> Get(int id);
        Task<AccountResponseDto> AddAccount(AccountRequestDto account);
        Task<(AccountResponseDto, string)> Authenticate(string id, string password);
        Task ForgotPassword(string email, string currentUrl);
    }
}
