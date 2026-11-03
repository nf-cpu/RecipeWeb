using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ChatMessageDto
    {
        [Required]
        [RegularExpression("^(user|ai)$", ErrorMessage = "Role must be user or assistant")]
        public string Role { get; set; } = "";

        [Required(ErrorMessage = "Message text is required")]
        [MaxLength(2000)]
        public string Text { get; set; } = "";
    }
}
