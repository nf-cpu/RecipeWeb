using BLL;
using BLL.Services;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RecipeWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipesController : ControllerBase
    {
        private readonly IRecipeService irecipeService;

        public RecipesController(IRecipeService irecipeService)
        {
            this.irecipeService = irecipeService;
        }

        [HttpPost]
        public IActionResult Add([FromForm] RecipePostDto dto)
        {
            try
            {
                return Ok(irecipeService.AddRecipe(dto));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

       
    }
}
