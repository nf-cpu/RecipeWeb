using BLL;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RecipeWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipesController : ControllerBase
    {
        private readonly IRecipeRepository _recipeRepository;

        public RecipesController(IRecipeRepository recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }

        [HttpPost]
        public IActionResult Add([FromForm] RecipePostDto dto)
        {
            try
            {
                return Ok(_recipeRepository.Add(dto));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

       
    }
}
