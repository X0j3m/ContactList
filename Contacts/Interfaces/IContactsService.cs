using Contacts.DTOs;
using Contacts.Model;

namespace Contacts.Interfaces
{
    public interface IContactsService
    {
        ICollection<ContactDTO> GetAll();
        ContactDTO? GetById(Guid id);
        Guid Create(CreateContactDTO contactDto);
        void Update(UpdateContactDTO updateContactDto);
        void Delete(Guid id);
    }
}
