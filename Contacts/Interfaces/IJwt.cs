using Contacts.Security;

namespace Contacts.Interfaces
{
    public interface IJwt
    {
        JwtToken GenerateJwtToken(string username);
    }
}
