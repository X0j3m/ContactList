using Contacts.Interfaces;
using Contacts.Model;

namespace Contacts.Services
{
    public class CategoriesService : ICategoriesService
    {
        private readonly ICategoriesRepository _categoryRepository;

        public CategoriesService(ICategoriesRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public ICollection<Category> GetAllCategories()
        {
            return _categoryRepository.GetAll();
        }

        public Category? GetCategoryById(Guid Id)
        {
            try
            {
                return _categoryRepository.GetById(Id);
            }
            catch (KeyNotFoundException)
            {
                return null;
            }
        }
    }
}
