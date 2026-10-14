using AutoMapper;
using AutoMapper.QueryableExtensions;
using BLL.Services;
using DAL;
using DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class RecipeRepository:IRecipeRepository
    {
        private readonly RecipesDB _context;
        private readonly IMapper _mapper;
        private readonly IImageService _imageService;

        public RecipeRepository(RecipesDB context, IImageService imageService, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _imageService = imageService;
        }
        public RecipeDTO Add(RecipePostDto dto)
        {
            var recipe = _mapper.Map<Recipe>(dto);

            if (dto.ImageFile != null)
                recipe.Image = _imageService.Save(dto.ImageFile);   // רק קריאה לשירות

            // recipe.DateUpload = DateOnly.FromDateTime(DateTime.Now);
            _context.Recipes.Add(recipe);
            _context.SaveChanges();

            return _mapper.Map<RecipeDTO>(recipe);
        }

        public List<RecipeDTO> GetAllRecipes()
        {
           return _context.Recipes
                //מייצרת שאילתא, שבעצם מחפשת את האובייקט המקושר
              .ProjectTo<RecipeDTO>(_mapper.ConfigurationProvider)
              .ToList();
        }
        public RecipeDTO CreateRecipe(RecipeDTO dto)
        {
            var recipe = _mapper.Map<Recipe>(dto);


            _context.Recipes.Add(recipe);
            _context.SaveChanges();
            //אפשר להשתמש בinclude
            //אבל עדיף להשתמש בprojecto
            var created = _context.Recipes
                //תטען גם את קשרי הגומלין
                .Include(r => r.Category)
                .First(r => r.Id == recipe.Id);

            return _mapper.Map<RecipeDTO>(created);
        }

        public RecipeDTO? UpdateRecipe(int id, RecipeDTO dto)
        {
            var recipe = _context.Recipes
                .Include(r => r.Category)
                .FirstOrDefault(r => r.Id == id);

            if (recipe is null)
                return null;

            _mapper.Map(dto, recipe);
            _context.SaveChanges();
            return _mapper.Map<RecipeDTO>(recipe);
        }

        public RecipeDTO? GetRecipeById(int id)
        {
            throw new NotImplementedException();
        }

        public bool DeleteRecipe(int id)
        {
            throw new NotImplementedException();
        }
    }
}
