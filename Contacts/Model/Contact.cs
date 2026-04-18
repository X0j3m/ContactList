namespace Contacts.Model
{
    public record Contact
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public string Surname { get; init; }
        public string Email { get; init; }
        public string Password { get; init; }
        public Category? Category { get; init; }
        public SubCategory? SubCategory { get; init; }
        public string? CustomSubCategory { get; init; }
        public string Phone { get; init; }
        public DateOnly BirthDate { get; init; }
    }
}
