using Microsoft.EntityFrameworkCore;
using DAL;
using BLL;
using Microsoft.Extensions.DependencyInjection;
using BLL.Services;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);
var imagesFolder = Path.GetFullPath(Path.Combine(builder.Environment.ContentRootPath,builder.Configuration["ImagesFolder"]));
Directory.CreateDirectory(imagesFolder);   // יוצר את התיקייה אם אינה קיימת

// Services
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<RecipesDB>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<MappingProfile>();
});
builder.Services.AddScoped<IRecipeRepository, RecipeRepository>();
builder.Services.AddSingleton<IImageService>(new ImageService(imagesFolder));

var app = builder.Build();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(imagesFolder),
    RequestPath = "/images"
});
// HTTP pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();