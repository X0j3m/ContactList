using System.Text.RegularExpressions;

namespace Contacts.Security
{
    public class PasswordValidator
    {
        public static bool MeetTheCriteria(string password)
        {
            if(string.IsNullOrWhiteSpace(password) || password.Length < 8)
            {
                return false;
            }
            var pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$";

            return Regex.IsMatch(password, pattern);
        }
    }
}
