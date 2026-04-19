using Contacts.Model;

namespace Contacts.Interfaces
{
    public interface ISubCategoriesRepository
    {
        bool Exists(Guid id);
        ICollection<SubCategory> GetAll();
        SubCategory GetById(Guid id);
        ICollection<SubCategory> GetByCategoryId(Guid categoryId);
    }
}
