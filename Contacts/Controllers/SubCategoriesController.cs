using Contacts.DTOs;
using Contacts.Interfaces;
using Contacts.Model;
using Microsoft.AspNetCore.Mvc;

namespace Contacts.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubCategoriesController : ControllerBase
    {
        private readonly ISubCategoriesService _subCategoriesService;

        public SubCategoriesController(ISubCategoriesService subCategoriesService)
        {
            _subCategoriesService = subCategoriesService;
        }

        [HttpGet]
        public ActionResult<ICollection<SubCategoryDTO>> GetAll()
        {
            var subCategories = _subCategoriesService.GetAllSubCategories();
            var subCategoryDtos = subCategories.Select(
                sc => new SubCategoryDTO(sc.Id, sc.Name, sc.CategoryId)
            ).ToList();
            return Ok(subCategoryDtos);
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<SubCategoryDTO> GetById(Guid id)
        {
            var subCategory = _subCategoriesService.GetSubCategoryById(id);
            if (subCategory == null)
            {
                return NotFound();
            }
            return Ok(subCategory.ToDTO());
        }
    }
}
