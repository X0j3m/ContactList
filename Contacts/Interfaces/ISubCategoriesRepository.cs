using Contacts.Model;

namespace Contacts.Interfaces
{
    public interface ISubCategoriesRepository
    {
        bool Exists(Guid Id);
        ICollection<SubCategory> GetAll();
        SubCategory GetById(Guid Id);
        ICollection<SubCategory> GetByCategoryId(Guid CategoryId);
    }
}
