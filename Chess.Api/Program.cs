using ChesApi.Infrastructure;
using ChesApi.Infrastructure.MoveTypeStrategy;
using ChesApi.Infrastructure.MoveTypeStrategy.MoveTypes;
using ChesApi.Infrastructure.Repo;
using ChesApi.Infrastructure.Services;
using Chess.Core.Repo.Game;
using Microsoft.IdentityModel.Tokens;
using Chess.Core.Repo.UserRepository;
using System.Text;
using ChesApi.Infrastructure.MyContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ChesApi.Infrastructure.AutoMapper;
using Microsoft.AspNetCore.Identity;
using Chess.Core.Domain;
using Chess.Api.Controllers;
using Microsoft.AspNetCore.Http.Connections;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var authenticationSettings = new AuthenticationSettings();
builder.Configuration.GetSection("Authentication").Bind(authenticationSettings);
builder.Services.AddSingleton(authenticationSettings);
builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = "Bearer";
    option.DefaultScheme = "Bearer";
    option.DefaultChallengeScheme = "Bearer";
}).AddJwtBearer(cfg =>
{
    cfg.RequireHttpsMetadata = false;
    cfg.SaveToken = true;
    cfg.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = authenticationSettings.JwtIssuer,
        ValidAudience = authenticationSettings.JwtIssuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey)),
    };
});


builder.Services.AddControllers();
builder.Services.AddSignalR().AddJsonProtocol(option => option.PayloadSerializerOptions.PropertyNamingPolicy = null);
builder.Services.AddSingleton(AutoMapperConfig.Initialize());
builder.Services.AddScoped<IStrategy, BischopPromotion>();
builder.Services.AddScoped<IStrategy, KnightPromotion>();
builder.Services.AddScoped<IStrategy, RookPromotion>();
builder.Services.AddScoped<IStrategy, QueenPromotion>();
builder.Services.AddScoped<IStrategy, StandartMove>();
builder.Services.AddScoped<IStrategyFactory<IStrategy>, StrategyFactory<IStrategy>>();
builder.Services.AddScoped<IFigureRepository, FigureRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IMoveService, MoveService>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(options =>
{ 
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "ChessApi", Version = "v1" });
    // here some other configurations maybe...
    options.AddSignalRSwaggerGen();
});
builder.Services.AddDbContext<ChessApiContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("MyChessApiCS")));

var app = builder.Build();

app.MapHub<LobbyGameHub>("/ChessHub", option => option.Transports = HttpTransportType.WebSockets | HttpTransportType.LongPolling);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
