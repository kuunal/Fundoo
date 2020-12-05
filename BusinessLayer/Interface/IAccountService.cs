using ModelLayer;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface IAccountService
    {
        Task<List<Account>> Get();
        Task<Account> AddAccount(Account account);

    }
}
