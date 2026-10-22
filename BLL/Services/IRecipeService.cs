using DTO;
using System.Collections.Generic;

namespace BLL.Services
{
    public interface IRecipeService
    {
        List<RecipeDTO> GetAllRecipes();
        RecipeDTO? GetRecipeById(int id);
        RecipeDTO AddRecipe(RecipePostDto dto);
        RecipeDTO? UpdateRecipe(int id, RecipeDTO dto);
        bool DeleteRecipe(int id, int userId);

    }
}