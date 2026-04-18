using Contacts.Interfaces;
using Contacts.Model;

namespace Contacts.Data
{
    public class SubCategoriesRepository : ISubCategoriesRepository
    {
        private readonly ContactsDbContext _dbContext;

        public SubCategoriesRepository(ContactsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool Exists(Guid Id)
        {
            return _dbContext.SubCategories
                .Any(sc => sc.Id == Id);
        }   

        public ICollection<SubCategory> GetAll()
        {
            return _dbContext.SubCategories
                .ToList();
        }

        public ICollection<SubCategory> GetByCategoryId(Guid CategoryId)
        {
            return _dbContext.SubCategories
                .Where(sc => sc.CategoryId == CategoryId)
                .ToList();
        }

        public SubCategory GetById(Guid Id)
        {
            if(!Exists(Id))
            {
                throw new KeyNotFoundException($"SubCategory with Id {Id} does not exist.");
            }
            return _dbContext.SubCategories
                .First(sc => sc.Id == Id);
        }
    }
}
