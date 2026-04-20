namespace Contacts.Interfaces
{
    public interface IJwt
    {
        string GenerateJwtToken(string username);
    }
}
