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
using ChesApi.Infrastructure.AutoMapper;
using Microsoft.AspNetCore.Identity;
using Chess.Core.Domain;
using Chess.Api.Controllers;
using Microsoft.AspNetCore.Http.Connections;
using ChesApi.Infrastructure.Hub;
using Microsoft.AspNetCore.Authentication.JwtBearer;

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
    cfg.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            var accesToken = context.Request.Query["access_token"];
            var path = context.HttpContext.Request.Path;
            if (!string.IsNullOrEmpty(accesToken) && path.StartsWithSegments("/Hubs"))
            {
                context.Token = accesToken;
            }
            return Task.CompletedTask;
        }
    };
    cfg.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = authenticationSettings.JwtIssuer,
        ValidAudience = authenticationSettings.JwtIssuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey)),
    };
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.WithOrigins("")
                .AllowAnyHeader()
                .WithMethods("GET", "POST")
                .AllowCredentials();
        });
});
builder.Services.AddControllers();
builder.Services.AddSignalR().AddJsonProtocol(option => option.PayloadSerializerOptions.PropertyNamingPolicy = null);
builder.Services.AddSingleton(AutoMapperConfig.Initialize());
builder.Services.AddScoped<IStrategy, BishopPromotion>();
builder.Services.AddScoped<IStrategy, KnightPromotion>();
builder.Services.AddScoped<IStrategy, RookPromotion>();
builder.Services.AddScoped<IStrategy, QueenPromotion>();
builder.Services.AddScoped<IStrategy, StandardMove>();
builder.Services.AddScoped<IStrategyFactory<IStrategy>, StrategyFactory<IStrategy>>();
builder.Services.AddScoped<IFigureRepository, FigureRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddSingleton<IHubLobby, HubLobby>();
builder.Services.AddScoped<IMappedHubLobby, MappedHubLobby>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ChessApiContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("MyChessApiCS")));

var app = builder.Build();

app.UseCors();

app.MapHub<LobbyGameHub>("/Hubs/ChessHub", option => option.Transports = HttpTransportType.WebSockets | HttpTransportType.LongPolling);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
