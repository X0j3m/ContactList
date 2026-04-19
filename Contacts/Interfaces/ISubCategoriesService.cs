using Contacts.Model;

namespace Contacts.Interfaces
{
    public interface ISubCategoriesService
    {
        ICollection<SubCategory> GetAllSubCategories();
        SubCategory? GetSubCategoryById(Guid id);
    }
}
