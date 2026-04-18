using Contacts.Model;

namespace Contacts.Interfaces
{
    public interface ICategoriesService
    {
        ICollection<Category> GetAllCategories();
        Category? GetCategoryById(Guid id);
        ICollection<SubCategory> GetSubCategoriesByCategoryId(Guid categoryId);
    }
}
