using Contacts.DTOs;
using Contacts.Interfaces;
using Contacts.Model;

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
                if (contactDto.CategoryId != null && contactDto.SubcategoryId != null)
                {
                    var category = _categoriesRepository.GetById(Guid.Parse(contactDto.CategoryId));
                    var subcategory = _subcategoriesRepository.GetById(Guid.Parse(contactDto.SubcategoryId));
                    var contact = new Contact
                    {
                        Id = Guid.NewGuid(),
                        Name = contactDto.Name,
                        Surname = contactDto.Surname,
                        Email = contactDto.Email,
                        Password = contactDto.Password,
                        Category = category,
                        SubCategory = subcategory,
                        Phone = contactDto.Phone,
                        BirthDate = DateOnly.Parse(contactDto.BirthDate)
                    };
                    _contactsRepository.Create(contact);
                    return contact.Id;
                }
                else
                {
                    var contact = new Contact
                    {
                        Id = Guid.NewGuid(),
                        Name = contactDto.Name,
                        Surname = contactDto.Surname,
                        Email = contactDto.Email,
                        Password = contactDto.Password,
                        CustomSubCategory = contactDto.CustomSubCategory,
                        Phone = contactDto.Phone,
                        BirthDate = DateOnly.Parse(contactDto.BirthDate)
                    };
                    _contactsRepository.Create(contact);
                    return contact.Id;
                }
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
                if (updateContactDto.CategoryId != null && updateContactDto.SubcategoryId != null)
                {
                    var contact = new Contact
                    {
                        Id = id,
                        Name = updateContactDto.Name,
                        Surname = updateContactDto.Surname,
                        Email = updateContactDto.Email,
                        Password = updateContactDto.Password,
                        CategoryId = Guid.Parse(updateContactDto.CategoryId),
                        SubCategoryId = Guid.Parse(updateContactDto.SubcategoryId),
                        Phone = updateContactDto.Phone,
                        BirthDate = DateOnly.Parse(updateContactDto.BirthDate)
                    };
                    _contactsRepository.Update(contact);
                    return contact.Id;
                }
                else
                {
                    var contact = new Contact
                    {
                        Id = id,
                        Name = updateContactDto.Name,
                        Surname = updateContactDto.Surname,
                        Email = updateContactDto.Email,
                        Password = updateContactDto.Password,
                        CustomSubCategory = updateContactDto.CustomSubCategory,
                        Phone = updateContactDto.Phone,
                        BirthDate = DateOnly.Parse(updateContactDto.BirthDate)
                    };
                    _contactsRepository.Update(contact);
                    return contact.Id;
                }
            }
            catch (ArgumentException) { }
            catch (KeyNotFoundException) { }
            return Guid.Empty;
        }
    }
}
