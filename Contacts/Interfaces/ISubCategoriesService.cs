using Contacts.Model;

namespace Contacts.Interfaces
{
    public interface ISubCategoriesService
    {
        ICollection<SubCategory> GetAllSubCategories();
        ICollection<SubCategory> GetAllSubCategoriesByCategoryId(Guid CategoryId);
        SubCategory? GetSubCategoryById(Guid Id);
    }
}
