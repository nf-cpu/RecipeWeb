using Microsoft.AspNetCore.Http;
using DTO.Validation;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class RecipePostDto
    {
        [Required(ErrorMessage = "Recipe title is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Recipe title must be between 2 and 100 characters")]
        public string Title { get; set; } = "";

        [Required(ErrorMessage = "Ingredients are required")]
        public string Ingredients { get; set; } = "";

        [Required(ErrorMessage = "Preparation instructions are required")]
        [MinLength(10, ErrorMessage = "Preparation instructions are too short")]
        public string Content { get; set; } = "";

        [StringLength(50, ErrorMessage = "Preparation time is too long")]
        public string? PrepareTime { get; set; }

        public int UserId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please select a category")]
        public int CategoryId { get; set; }

        [AllowedExtensions(".jpg", ".jpeg", ".png", ".webp")]
        [MaxFileSize(5)]
        public IFormFile? ImageFile { get; set; }
    }
}
