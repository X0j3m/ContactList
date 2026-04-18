namespace Contacts.Model
{
    public class SubCategory
    {
        public Guid Id { get; init; }
        public string Name { get; init; }

        public Guid CategoryId { get; init; }
        public Category Category { get; init; } = default!;
    }
}
