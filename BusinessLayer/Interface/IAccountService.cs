using ModelLayer;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface IAccountService
    {
        Task<Account> Get(int id);
        Task<Account> AddAccount(Account account);
        Task<Account> Authenticate(string id, string password);
    }
}
