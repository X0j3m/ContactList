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

        public SubCategory? GetSubCategoryById(Guid Id)
        {
            try
            {
                return _subCategoryRepository.GetById(Id);
            }
            catch (KeyNotFoundException)
            {
                return null;
            }
        }
    }
}