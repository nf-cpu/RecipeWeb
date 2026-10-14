using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public interface IRecipeRepository
    {
        RecipeDTO Add(RecipePostDto dto);
        List<RecipeDTO> GetAllRecipes();
        RecipeDTO? GetRecipeById(int id);
        RecipeDTO CreateRecipe(RecipeDTO dto);
        RecipeDTO? UpdateRecipe(int id, RecipeDTO dto);
        bool DeleteRecipe(int id);

    }
}
