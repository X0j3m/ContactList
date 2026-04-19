using Contacts.Model;
using Contacts.Security;
using System.Net.Mail;

namespace Contacts.DTOs
{
    public record ContactDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? CategoryId { get; set; }
        public string? SubcategoryId { get; set; }
        public string? CustomSubCategory { get; set; }
        public string Phone { get; set; }
        public string BirthDate { get; set; }

        public static Contact? ToEntity(ContactDTO dto)
        {
            // Validate password, if meets the criteria of strong password
            var validPassword = PasswordValidator.MeetTheCriteria(dto.Password);
            if (!validPassword) {
                return null;
            }
            try
            {
                // Validate email format, if invalid, a FormatException will be thrown
                new MailAddress(dto.Email);

                return new Contact
                {
                    Id = dto.Id,
                    Name = dto.Name,
                    Surname = dto.Surname,
                    Email = dto.Email,
                    Password = dto.Password,
                    CategoryId = string.IsNullOrEmpty(dto.CategoryId) ? null : Guid.Parse(dto.CategoryId),
                    SubCategoryId = string.IsNullOrEmpty(dto.SubcategoryId) ? null : Guid.Parse(dto.SubcategoryId),
                    CustomSubCategory = dto.CustomSubCategory,
                    Phone = dto.Phone,
                    BirthDate = DateOnly.Parse(dto.BirthDate)
                };
            }catch (FormatException)
            {
                return null;
            }
        }
    }
    public record CreateContactDTO
    {
        public string Name { get; init; }
        public string Surname { get; init; }
        public string Email { get; init; }
        public string Password { get; init; }
        public string? CategoryId { get; init; }
        public string? SubcategoryId { get; init; }
        public string? CustomSubCategory { get; init; }
        public string Phone { get; init; }
        public string BirthDate { get; init; }
    }
    public record UpdateContactDTO
    {
        public string Name { get; init; }
        public string Surname { get; init; }
        public string Email { get; init; }
        public string Password { get; init; }
        public string? CategoryId { get; init; }
        public string? SubcategoryId { get; init; }
        public string? CustomSubCategory { get; init; }
        public string Phone { get; init; }
        public string BirthDate { get; init; }
    }
}
