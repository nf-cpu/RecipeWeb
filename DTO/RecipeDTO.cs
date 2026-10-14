using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class RecipeDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        // פסיק מפריד בין המרכיבים
        public string Ingredients { get; set; }
        public string Content { get; set; }
        public string PrepareTime { get; set; }
        public string Image { get; set; }

        public int Score { get; set; }

        public int UserId { get; set; }

        public int CategoryId { get; set; }
      
        public string CategoryName { get; set; }
    }
}
