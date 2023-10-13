using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json.Linq;
using Stock.Model;
using Stock.Repository;

namespace Stock.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : BaseController
    {
        public CategoryController(RepositoryWrapper repo, IMemoryCache cache) : base(repo, cache)
        {

        }
        [HttpGet("GetCategories")]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            var categories = await repo.CategoryRepository.GetCategoriesAsync();
            if (categories == null || !categories.Any())
                return NotFound("Kategori bulunamadı");
            return Ok(categories);
        }
        [HttpPost("Save")]
        public dynamic Save([FromBody] dynamic model)
        {
            dynamic json = JObject.Parse(model.GetRawText());
            Category category = new Category()
            {
                Id = json.Id,
                CategoryName = json.CategoryName,



            };
            if (category.Id > 0)
            {
                repo.CategoryRepository.Update(category);
            }
            else
            {
                repo.CategoryRepository.Create(category);
            }

            repo.SaveChanges();

            return new
            {

                success = true,
                message = "Property saved successfully"
            };


        }
        [HttpPost("DeleteCategory")]
        public async Task<ActionResult> DeleteCategory(int categoryId)
        {
            var category = await repo.CategoryRepository.FindByIdAsync(categoryId);
            if (category == null)
                return NotFound("Kategori Bulunamadı");
            repo.CategoryRepository.Delete(category);
            repo.SaveChanges();
            return Ok("Kategori başarı ile silindi");
        }
    }
}
