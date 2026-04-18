using Contacts.Model;

namespace Contacts.Interfaces
{
    public interface ICategoriesService
    {
        ICollection<Category> GetAllCategories();
        Category? GetCategoryById(Guid Id);
        ICollection<SubCategory> GetSubCategoriesByCategoryId(Guid CategoryId);
    }
}
