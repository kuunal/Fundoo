using ModelLayer;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface IAccountRepository
    {
        Task<List<Account>> Get();

        Task<Account> AddAccount(Account account);
    }
}
