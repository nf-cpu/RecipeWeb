using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "Email is required"), EmailAddress(ErrorMessage = "Email address is invalid")]
        public string Email { get; set; } = "";

        [Required(ErrorMessage = "Password is required"), MinLength(8, ErrorMessage = "Password must be at least 8 characters")]
        public string Password { get; set; } = "";
    }
}
