using Contacts.Interfaces;
using Contacts.Model;

namespace Contacts.Data
{
    public class ContactsRepository : IContactsRepository
    {
        private readonly ContactsDbContext _dbContext;

        public ContactsRepository(ContactsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Create(Contact contact)
        {
            _dbContext.Contacts.Add(contact);
            _dbContext.SaveChanges();
            return;
        }

        public void Delete(Contact contact)
        {
            if (Exists(contact.Id))
            {
                _dbContext.Contacts.Remove(contact);
                _dbContext.SaveChanges();
            }
            return;
        }

        public bool Exists(Guid id)
        {
            return _dbContext.Contacts.Any(c => c.Id == id);
        }

        public bool Exists(string email)
        {
            return _dbContext.Contacts.Any(c => c.Email == email);
        }

        public ICollection<Contact> GetAll()
        {
            return _dbContext.Contacts.ToList();
        }

        public Contact GetById(Guid id)
        {
            if (!Exists(id))
            {
                throw new KeyNotFoundException($"Contact with id {id} not found.");
            }
            return _dbContext.Contacts.First(c => c.Id == id);
        }

        public void Update(Contact contact)
        {
            if (!Exists(contact.Id))
            {
                throw new KeyNotFoundException($"Contact with id {contact.Id} not found.");
            }
            if (Exists(contact.Email))
            {
                throw new InvalidOperationException($"Contact with email {contact.Email} already exists.");
            }
            _dbContext.Contacts.Update(contact);
            _dbContext.SaveChanges();
            return;
        }
    }
}
