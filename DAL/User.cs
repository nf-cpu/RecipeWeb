using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class User
    {
        public int Id { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; } //סיסמא מוצפנת
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public int? Status { get; set; }
        public string? Role { get; set; }

        public List<Recipe> Recipes { get; set; }

    }


}
