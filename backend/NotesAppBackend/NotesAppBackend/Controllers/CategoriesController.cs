using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotesAppBackend.Models;
using NotesAppBackend.Services;
using static Azure.Core.HttpHeader;

namespace NotesAppBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private CategoryService _categoryService;
        public CategoriesController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> getAllCategories()
        {
            try
            {
                List<Category> categories = await _categoryService.getAllCategories();
                return StatusCode(StatusCodes.Status200OK, categories);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Internal Server Error", error = ex.Message });
            }
        }


        [HttpPost]
        public async Task<IActionResult> createCategory([FromBody] Category category)
        {
            if (category == null || string.IsNullOrEmpty(category.Name))
            {
                return BadRequest(new { message = "The note is null" });
            }

            try
            {
                bool isCreated = await _categoryService.createCategory(category);
                if (isCreated)
                {
                    return StatusCode(StatusCodes.Status200OK, category);
                }
                else
                {
                    return StatusCode(StatusCodes.Status409Conflict, new { message = "The category cannot be created because a category with the same name already exists" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Internal Server Error", error = ex.Message });
            }
        }


        [HttpPut]
        [Route("editActivatedStatus/{idCategory}")]
        public async Task<IActionResult> editActivatedStatus(int idCategory)
        {
            if (idCategory <= 0)
            {
                return BadRequest(new { message = "Invalid category ID" });
            }

            try
            {
                bool isEdited = await _categoryService.editActivatedStatus(idCategory);
                if (isEdited)
                {
                    return StatusCode(StatusCodes.Status200OK, new { message = "The category was edited successfully" });
                }
                else
                {
                    return StatusCode(StatusCodes.Status404NotFound, new { message = "The category does not exist" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Internal Server Error", error = ex.Message });
            }
        }

    }
}
