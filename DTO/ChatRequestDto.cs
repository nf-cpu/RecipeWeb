using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ChatRequestDto
    {
        [Required]
        [MinLength(1, ErrorMessage = "At least one message is required")]
        public List<ChatMessageDto> Messages { get; set; } = new();
    }
}
