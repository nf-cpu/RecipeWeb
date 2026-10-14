using AutoMapper;
using System.Reflection.Metadata;
using DAL;
using DTO;

namespace BLL
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDTO>();
            CreateMap<Recipe, RecipeDTO>()
           .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));
            CreateMap<RecipePostDto, Recipe>();
        }
    }
}