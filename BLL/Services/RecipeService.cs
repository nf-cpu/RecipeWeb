using AutoMapper;
using BLL.Exceptions;
using DAL;
using DTO;
using System.Collections.Generic;

namespace BLL.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly IImageService _imageService;

        public RecipeService(
            IRecipeRepository recipeRepository,
            ICategoryRepository categoryRepository,
            IMapper mapper,
            IImageService imageService)
        {
            _recipeRepository = recipeRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _imageService = imageService;
        }

        public List<RecipeDTO> GetAllRecipes()
        {
            var recipes = _recipeRepository.GetAll();
            return _mapper.Map<List<RecipeDTO>>(recipes);
        }

        public RecipeDTO GetRecipeById(int id)
        {
            var recipe = _recipeRepository.GetById(id);
            if (recipe == null)
                throw new NotFoundException($"Recipe with id {id} was not found");

            return _mapper.Map<RecipeDTO>(recipe);
        }

        public RecipeDTO AddRecipe(RecipePostDto dto)
        {
            if (!_categoryRepository.Exists(dto.CategoryId))
                throw new BusinessRuleException("The selected category does not exist");

            var recipe = _mapper.Map<Recipe>(dto);

            if (dto.ImageFile != null)
            {
                recipe.Image = _imageService.Save(dto.ImageFile);
            }

            try
            {
                _recipeRepository.Add(recipe);
            }
            catch
            {
                if (recipe.Image != null)
                    _imageService.Delete(recipe.Image);

                throw;
            }

            return _mapper.Map<RecipeDTO>(recipe);
        }

        public RecipeDTO? UpdateRecipe(int id, RecipeDTO dto)
        {
            var existingRecipe = _recipeRepository.GetById(id);
            if (existingRecipe == null)
                return null;

            _mapper.Map(dto, existingRecipe);
            _recipeRepository.Update(existingRecipe);

            return _mapper.Map<RecipeDTO>(existingRecipe);
        }

        public bool DeleteRecipe(int id)
        {
            return _recipeRepository.Delete(id);
        }
    }
}