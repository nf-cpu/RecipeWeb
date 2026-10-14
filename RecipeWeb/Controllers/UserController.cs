using AutoMapper;
using BLL;
using DAL;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RecipeWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private RecipesDB db;
        private IMapper mappingProfile;

        public UserController(RecipesDB recipesDB, IMapper mappingProfile)
        {
            db=recipesDB;
            this.mappingProfile=mappingProfile;
        }

        [HttpGet]
       public List<UserDTO> GetUserDTOs()
        {
            //UserDTO userDTO = new UserDTO();
            //User user = .First();

            //userDTO.PhoneNumber=user.PhoneNumber;
            //userDTO.FirstName=user.FirstName;

            //....... 

            return mappingProfile.Map<List<UserDTO>>(db.Users);

            
        }
    }
}
