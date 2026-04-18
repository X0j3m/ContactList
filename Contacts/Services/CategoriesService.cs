using Contacts.Interfaces;
using Contacts.Model;

namespace Contacts.Services
{
    public class CategoriesService : ICategoriesService
    {
        private readonly ICategoriesRepository _categoryRepository;
        private readonly ISubCategoriesRepository _subCategoryRepository;

        public CategoriesService(
            ICategoriesRepository categoryRepository,
            ISubCategoriesRepository subCategoriesRepository)
        {
            _categoryRepository = categoryRepository;
            _subCategoryRepository = subCategoriesRepository;
        }

        public ICollection<Category> GetAllCategories()
        {
            return _categoryRepository.GetAll();
        }

        public Category? GetCategoryById(Guid id)
        {
            try
            {
                return _categoryRepository.GetById(id);
            }
            catch (KeyNotFoundException)
            {
                return null;
            }
        }

        public ICollection<SubCategory> GetSubCategoriesByCategoryId(Guid categoryId)
        {
            var category = GetCategoryById(categoryId);
            if(category == null)
            {
                return new List<SubCategory>();
            }
            return _subCategoryRepository.GetByCategoryId(categoryId);
        }
    }
}
