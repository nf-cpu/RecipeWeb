using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class RecipePostDto
    {
        public string Title { get; set; }
        public string Ingredients { get; set; }
        public string Content { get; set; }
        public string PrepareTime { get; set; }
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public IFormFile ImageFile { get; set; }   // הקובץ
    }
}
