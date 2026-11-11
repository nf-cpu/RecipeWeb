using Microsoft.EntityFrameworkCore;
using DAL;
using BLL;
using Microsoft.Extensions.DependencyInjection;
using BLL.Services;
using RecipeWeb.Filters;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.AI;
using OpenAI;
using System.ClientModel;

var builder = WebApplication.CreateBuilder(args);


//--------AI Agent
var apiKey = builder.Configuration["GEMINI:APIKEY"];
    //?? throw new InvalidOperationException("Gemini:ApiKey is missing");

// הכתובת של Gemini שמדברת בפורמט של OpenAI
var options = new OpenAIClientOptions
{
    Endpoint = new Uri("https://generativelanguage.googleapis.com/v1beta/openai/")
};

IChatClient innerClient = new OpenAIClient(new ApiKeyCredential(apiKey), options)
    .GetChatClient("gemini-2.5-flash")
    .AsIChatClient();

builder.Services.AddChatClient(innerClient)
    .UseFunctionInvocation();   // מריץ את הכלים שהמודל מבקש

builder.Services.AddScoped<IAgentService, AgentService>();

//-------------JWT

var jwtKey = builder.Configuration["Jwt:Key"];
if (string.IsNullOrWhiteSpace(jwtKey))
    throw new InvalidOperationException("Jwt:Key is missing. Set the environment variable Jwt__Key.");

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],

            ValidateAudience = true,
            ValidAudience = builder.Configuration["Jwt:Audience"],

            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero,

            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };
    });

builder.Services.AddAuthorization();



var imagesFolder = Path.GetFullPath(Path.Combine(builder.Environment.ContentRootPath,builder.Configuration["ImagesFolder"]));
Directory.CreateDirectory(imagesFolder);   // ���� �� ������� �� ���� �����

// Services
builder.Services.AddControllers(options =>
{
    options.Filters.Add<GlobalExceptionFilter>();
});

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

//DAL
builder.Services.AddScoped<IRecipeRepository, RecipeRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

// BLL
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddSingleton<IImageService>(new ImageService(imagesFolder));
builder.Services.AddScoped<IRecipeService, RecipeService>();


//-------weatherService
builder.Services.AddHttpClient<WeatherService>(c =>
{
    c.BaseAddress = new Uri("https://api.open-meteo.com/");
    c.Timeout = TimeSpan.FromSeconds(10);
});


var app = builder.Build();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(imagesFolder),
    RequestPath = "/images"
});
// HTTP pipeline
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
