using Contacts.DTOs;
using Contacts.Interfaces;
using Contacts.Model;
using System.Net.Mail;

namespace Contacts.Services
{
    public class ContactsService : IContactsService
    {
        private readonly IContactsRepository _contactsRepository;
        private readonly ICategoriesRepository _categoriesRepository;
        private readonly ISubCategoriesRepository _subcategoriesRepository;

        public ContactsService(
            IContactsRepository contactsRepository,
            ICategoriesRepository categoriesRepository,
            ISubCategoriesRepository subcategoriesRepository)
        {
            _contactsRepository = contactsRepository;
            _categoriesRepository = categoriesRepository;
            _subcategoriesRepository = subcategoriesRepository;
        }

        public Guid Create(CreateContactDTO contactDto)
        {
            try
            {
                var contact = ContactDTO.ToEntity(
                    new ContactDTO
                    {
                        Name = contactDto.Name,
                        Surname = contactDto.Surname,
                        Email = contactDto.Email,
                        Password = contactDto.Password,
                        CategoryId = contactDto.CategoryId,
                        SubcategoryId = contactDto.SubcategoryId,
                        CustomSubCategory = contactDto.CustomSubCategory,
                        Phone = contactDto.Phone,
                        BirthDate = contactDto.BirthDate
                    }
                    );
                if (contact == null)
                {
                    return Guid.Empty;
                }
                _contactsRepository.Create(contact);
                return contact.Id;
            }
            catch (ArgumentException) { return Guid.Empty; }
            catch (KeyNotFoundException) { return Guid.Empty; }
        }

        public bool Delete(Guid id)
        {
            try
            {
                _contactsRepository.Delete(id);
                return true;
            }
            catch (KeyNotFoundException)
            {
                return false;
            }
        }

        public ICollection<ContactDTO> GetAll()
        {
            return _contactsRepository.GetAll().Select(c => c.ToDTO()).ToList();
        }

        public ContactDTO? GetById(Guid id)
        {
            try
            {
                var found = _contactsRepository.GetById(id);
                return found.ToDTO();
            }
            catch (KeyNotFoundException)
            {
                return null;
            }
        }

        public Guid Update(Guid id, UpdateContactDTO updateContactDto)
        {
            try
            {
                var contact = ContactDTO.ToEntity(
                    new ContactDTO
                    {
                        Id = id,
                        Name = updateContactDto.Name,
                        Surname = updateContactDto.Surname,
                        Email = updateContactDto.Email,
                        Password = updateContactDto.Password,
                        CategoryId = updateContactDto.CategoryId,
                        SubcategoryId = updateContactDto.SubcategoryId,
                        CustomSubCategory = updateContactDto.CustomSubCategory,
                        Phone = updateContactDto.Phone,
                        BirthDate = updateContactDto.BirthDate
                    }
                    );
                if (contact == null)
                {
                    return Guid.Empty;
                }
                _contactsRepository.Update(contact);
                return contact.Id;
            }
            catch (ArgumentException) { return Guid.Empty; }
            catch (KeyNotFoundException) { return Guid.Empty; }
        }
    }
}
