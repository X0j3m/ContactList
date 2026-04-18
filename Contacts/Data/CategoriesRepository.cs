using Contacts.Interfaces;
using Contacts.Model;
using Microsoft.EntityFrameworkCore;

namespace Contacts.Data
{
    public class CategoriesRepository : ICategoriesRepository
    {
        private readonly ContactsDbContext _dbContext;

        public CategoriesRepository(ContactsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool Exists(Guid Id)
        {
            return _dbContext.Categories
                .Any(c => c.Id == Id);
        }

        public ICollection<Category> GetAll()
        {
            return _dbContext.Categories.ToList();
        }

        public Category GetById(Guid Id)
        {
            if (!Exists(Id))
            {
                throw new KeyNotFoundException($"Category with id {Id} does not exist.");
            }
            return _dbContext.Categories
                .First(c => c.Id == Id);
        }
    }
}
