using Contacts.DTOs;
using Contacts.Interfaces;
using Contacts.Model;
using Microsoft.AspNetCore.Mvc;

namespace Contacts.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoriesService _categoriesService;

        public CategoriesController(ICategoriesService categoriesService)
        {
            _categoriesService = categoriesService;
        }

        [HttpGet]
        public ActionResult<ICollection<CategoryDTO>> GetAll()
        {
            var categories = _categoriesService.GetAllCategories();
            var categoriesDtos = categories.Select(
                c => new CategoryDTO(c.Id, c.Name)
            ).ToList();
            return Ok(categoriesDtos);
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<CategoryDTO> GetById(Guid id)
        {
            var category = _categoriesService.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category.ToDTO());
        }

        [HttpGet]
        [Route("{id}/subcategories")]
        public ActionResult<ICollection<SubCategoryDTO>> GetSubCategoriesByCategoryId(Guid id)
        {
            var subCategories = _categoriesService.GetSubCategoriesByCategoryId(id);
            var subCategoriesDtos = subCategories.Select(
                sc => new SubCategoryDTO(sc.Id, sc.Name, sc.CategoryId)
            ).ToList();
            return Ok(subCategoriesDtos);
        }
    }
}
