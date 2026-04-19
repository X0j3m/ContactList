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
