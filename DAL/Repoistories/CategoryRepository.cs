namespace DAL
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly RecipesDB _context;

        public CategoryRepository(RecipesDB context)
        {
            _context = context;
        }

        public bool Exists(int id)
        {
            return _context.Categories.Any(category => category.Id == id);
        }
    }
}
