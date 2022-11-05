using ChesApi.Infrastructure.MoveTypeStrategy;
using ChesApi.Infrastructure.MoveTypeStrategy.MoveTypes;
using ChesApi.Infrastructure.Repo;
using ChesApi.Infrastructure.Services;
using Chess.Core.Repo.Game;
using Chess.Core.Repo.UserRepository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<IStrategy, BischopPromotion>();
builder.Services.AddScoped<IStrategy, KnightPromotion>();
builder.Services.AddScoped<IStrategy, RookPromotion>();
builder.Services.AddScoped<IStrategy, QueenPromotion>();
builder.Services.AddScoped<IStrategy, StandartMove>();
builder.Services.AddScoped<IStrategyFactory<IStrategy>, StrategyFactory<IStrategy>>();
builder.Services.AddScoped<IFigureRepository, FigureRepository>();
builder.Services.AddScoped<IGameRepository, GameRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserInGameRepository, UserInGameRepository>();
builder.Services.AddScoped<IMoveService, MoveService>();
builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
