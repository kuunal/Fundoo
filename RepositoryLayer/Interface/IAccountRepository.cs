using ModelLayer;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface IAccountRepository
    {
        Task<Account> Get(string id);
        Task<Account> Get(int id);


        Task<Account> AddAccount(Account account);
    }
}
