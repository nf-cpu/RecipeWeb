using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
        public class RecipesDB : DbContext
        {
            public RecipesDB(DbContextOptions options)
                : base(options)
            {
            }

            public DbSet<User> Users { get; set; }
            public DbSet<Recipe> Recipes { get; set; }
            public DbSet<Category> Categories { get; set; }

       
    }
}
