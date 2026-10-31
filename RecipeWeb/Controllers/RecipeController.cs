using BLL;
using BLL.Services;
using DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RecipeWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]//מאבטחת את כל הקונטרולר שרק משתמש מחובר יכול לבצע פעולות
    //משתמש מחובר=יש לו טוקן
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
            return Ok(irecipeService.AddRecipe(dto));
        }

        [HttpGet]
        public ActionResult<List<RecipeDTO>> GetAll()
        {
            return irecipeService.GetAllRecipes();
        }

        [HttpGet("{id:int}")]
        public ActionResult<RecipeDTO> GetById(int id)
        {
            return Ok(irecipeService.GetRecipeById(id));
        }
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            if (!irecipeService.DeleteRecipe(id, User.GetUserId()))
                return NotFound();

            return NoContent();
        }
    }
}
