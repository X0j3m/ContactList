using Contacts.Model;

namespace Contacts.Interfaces
{
    public interface ISubCategoriesRepository
    {
        bool Exists(Guid Id);
        ICollection<SubCategory> GetAll();
        ICollection<SubCategory> GetAllByCategoryId(Guid CategoryId);
        SubCategory GetById(Guid Id);
    }
}
