using Contacts.Model;

namespace Contacts.Interfaces
{
    public interface ICategoriesRepository
    {
        bool Exists(Guid Id);
        ICollection<Category> GetAll();
        Category GetById(Guid id);
    }
}
