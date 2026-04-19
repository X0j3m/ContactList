using Contacts.DTOs;

namespace Contacts.Model
{
    public record Contact
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public string Surname { get; init; }
        public string Email { get; init; }
        public string Password { get; init; }
        public Guid? CategoryId { get; set; }
        public Guid? SubCategoryId { get; set; }
        public string? CustomSubCategory { get; init; }
        public string Phone { get; init; }
        public DateOnly BirthDate { get; init; }

       
        public Category? Category { get; set; }
        public SubCategory? SubCategory { get; set; }

        public ContactDTO ToDTO()
        {
            return new ContactDTO
            {
                Id = Id,
                Name = Name,
                Surname = Surname,
                Email = Email,
                Password = Password,
                CategoryId = CategoryId.HasValue ? CategoryId.Value.ToString() : null,
                SubcategoryId = SubCategoryId.HasValue ? SubCategoryId.Value.ToString() : null,
                CustomSubCategory = CustomSubCategory,
                Phone = Phone,
                BirthDate = BirthDate.ToString("yyyy-MM-dd")
            };
        }
    }
}
