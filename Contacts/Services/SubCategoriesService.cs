using Contacts.Interfaces;
using Contacts.Model;

namespace Contacts.Services
{
    public class SubCategoriesService : ISubCategoriesService
    {
        private readonly ISubCategoriesRepository _subCategoryRepository;

        public SubCategoriesService(ISubCategoriesRepository subCategoryRepository)
        {
            _subCategoryRepository = subCategoryRepository;
        }

        public ICollection<SubCategory> GetAllSubCategories()
        {
            return _subCategoryRepository.GetAll();
        }

        public SubCategory? GetSubCategoryById(Guid id)
        {
            try
            {
                return _subCategoryRepository.GetById(id);
            }
            catch (KeyNotFoundException)
            {
                return null;
            }
        }
    }
}