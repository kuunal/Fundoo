using ModelLayer;
using System.Security.Claims;

namespace TokenAuthentication
{
    public interface ITokenManager
    {
        string Encode(Account account);
        ClaimsPrincipal Decode(string token);
    }
}
