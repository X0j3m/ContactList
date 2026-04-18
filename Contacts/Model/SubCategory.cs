using Contacts.DTOs;

namespace Contacts.Model
{
    public record SubCategory
    {
        public Guid Id { get; init; }
        public required string Name { get; init; }

        public Guid CategoryId { get; init; }
        public Category Category { get; init; } = default!;

        public SubCategoryDTO ToDTO()
        {
            return new SubCategoryDTO(Id, Name, CategoryId);
        }
    }
}
