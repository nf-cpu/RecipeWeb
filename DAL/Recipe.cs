using System.Xml.Linq;

namespace DAL
{
    public class Recipe
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
        public User User { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

    }

}