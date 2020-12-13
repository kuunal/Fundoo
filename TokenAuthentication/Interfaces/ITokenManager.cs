using ModelLayer;
using System.Security.Claims;

namespace TokenAuthentication
{
    public interface ITokenManager
    {
        string Encode(Account account, int JwtExpiryTime, byte[] secret);
        string Encode(Account account);

        ClaimsPrincipal Decode(string token);
        ClaimsPrincipal Decode(string token, byte[] secret);
    }
}
