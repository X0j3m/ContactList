using Contacts.Model;

namespace Contacts.Interfaces
{
    public interface IContactsRepository
    {
        bool Exists(Guid id);
        bool Exists(string email);
        ICollection<Contact> GetAll();
        Contact GetById(Guid id);
        void Create(Contact contact);
        void Update(Contact contact);
        void Delete(Contact contact);
    }
}
