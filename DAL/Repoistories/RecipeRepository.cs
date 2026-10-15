using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DAL
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly RecipesDB _context;

        public RecipeRepository(RecipesDB context)
        {
            _context = context;
        }

        public List<Recipe> GetAll()
        {
            return _context.Recipes
                .Include(r => r.Category)
                .Include(r => r.User)
                .ToList();
        }

        public Recipe? GetById(int id)
        {
            return _context.Recipes
                .Include(r => r.Category)
                .Include(r => r.User)
                .FirstOrDefault(r => r.Id == id);
        }

        public void Add(Recipe recipe)
        {
            _context.Recipes.Add(recipe);
            _context.SaveChanges();
        }

        public void Update(Recipe recipe)
        {
            _context.Recipes.Update(recipe);
            _context.SaveChanges();
        }

        public bool Delete(int id)
        {
            var recipe = _context.Recipes.Find(id);
            if (recipe == null)
                return false;

            _context.Recipes.Remove(recipe);
            _context.SaveChanges();
            return true;
        }
    }
}