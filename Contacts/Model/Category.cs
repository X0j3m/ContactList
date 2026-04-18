namespace Contacts.Model
{
    public class Category
    {
        public Guid Id { get; init; }
        public string Name { get; init; }

        public ICollection<SubCategory> SubCategories { get; init; } = new List<SubCategory>();
    }
}
