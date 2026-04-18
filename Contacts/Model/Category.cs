using Contacts.DTOs;

namespace Contacts.Model
{
    public record Category
    {
        public Guid Id { get; init; }
        public required string Name { get; init; }

        public ICollection<SubCategory> SubCategories { get; init; } = new List<SubCategory>();

        public CategoryDTO ToDTO()
        {
            return new CategoryDTO(Id, Name);
        }
    }
}
