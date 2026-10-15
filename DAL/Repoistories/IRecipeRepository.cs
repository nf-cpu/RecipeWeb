using System.Collections.Generic;

namespace DAL
{
    public interface IRecipeRepository
    {
        List<Recipe> GetAll();
        Recipe? GetById(int id);
        void Add(Recipe recipe);
        void Update(Recipe recipe);
        bool Delete(int id);
    }
}