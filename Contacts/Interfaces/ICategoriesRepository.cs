using Contacts.Model;

namespace Contacts.Interfaces
{
    public interface ICategoriesRepository
    {
        bool Exists(Guid id);
        ICollection<Category> GetAll();
        Category GetById(Guid id);
    }
}
