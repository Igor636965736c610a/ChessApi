using ChesApi.Infrastructure.DTO;
using ChesApi.Infrastructure.Factory;
using ChesApi.Infrastructure.Hub;
using ChesApi.Infrastructure.MoveTypeStrategy.Enum;
using ChesApi.Infrastructure.Services;
using Chess.Api.GetClaims;
using Chess.Core.Domain;
using Chess.Core.Domain.EnumsAndStructs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace Chess.Api.Controllers
{
    [Authorize]
    public class LobbyGameHub : Hub
    {
        private readonly IHubLobby _hubLobby;
        private readonly IGameService _gameService;
        public LobbyGameHub(IHubLobby hubLobby, IGameService gameService)
        {
            _hubLobby = hubLobby;
            _gameService = gameService;
        }
        public override Task OnConnectedAsync()
        {
            return Clients.Caller.SendAsync("Witaj", _hubLobby.GetWaitingPlayers());
        }
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var leavingPlayer = _hubLobby.GetPlayer(Context.ConnectionId);
            if(_hubLobby.GetGame(leavingPlayer.GameId.ToString()) is not null)
            {
                if (leavingPlayer.WhiteColor)
                    await Clients.Group(leavingPlayer.GameId.ToString()).SendAsync("White Player Win");
                else
                    await Clients.Group(leavingPlayer.GameId.ToString()).SendAsync("Black Player Win");
                var game = _hubLobby.GetGame(leavingPlayer.GameId.ToString());
                await _hubLobby.RemoveGame(leavingPlayer.GameId.ToString());
                var player1 = game.Player1;
                var player2 = game.Player2;
                await _hubLobby.RemovePlayer(player1.ConntectionId);
                await _hubLobby.RemovePlayer(player2.ConntectionId);
            }
        }
        public async Task CreateRoom()
        {
            var userId = GetClaimsProperty.GetUserId(Context);
            var name = GetClaimsProperty.GetName(Context);
            var player = Factory.GetPlayer(userId, name, Context.ConnectionId, true, true);
            await _hubLobby.AddWaitingPlayer(player);
            await _hubLobby.AddPlayer(Context.ConnectionId, player);
            await Clients.All.SendAsync("AddRoom", player);
        }

        public async Task JoinRoom(string roomId)
        {
            var userId = GetClaimsProperty.GetUserId(Context);
            var name = GetClaimsProperty.GetName(Context);
            var player1 = _hubLobby.GetPlayer(roomId);
            if(player1 == null)
            {
                await Clients.Caller.SendAsync("Nie ma takiej gry!");
                return;
            }
            var player2 = Factory.GetPlayer(userId, name, Context.ConnectionId, false, false);
            var game = Factory.GetGame(player1, player2);
            await _hubLobby.RemoveWaitingPlayer(player1);
            await _hubLobby.AddGame(game.Id.ToString(), game);
            await _hubLobby.AddPlayer(Context.ConnectionId, player2);
            await Groups.AddToGroupAsync(game.Player1.ConntectionId, groupName: game.Id.ToString());
            await Groups.AddToGroupAsync(game.Player2.ConntectionId, groupName: game.Id.ToString());
            await Clients.Group(game.Id.ToString()).SendAsync("Start");
        }

        public async Task Move(Vector2 current, Vector2 target, MoveType moveType)
        {
            Console.WriteLine("weszlo do metody");
            var player = _hubLobby.GetPlayer(Context.ConnectionId);
            var game = _hubLobby.GetGame(player.GameId.ToString());
            if (player.HasMove)
            {
                try
                {
                    Console.WriteLine("move");
                    var moveResponse = _gameService.Move(moveType, target, current, game);
                    game.Player1.HasMove = !game.Player1.HasMove;
                    game.Player2.HasMove = !game.Player2.HasMove;
                    game.WhiteColor = !game.WhiteColor;
                    await Clients.Group(game.Id.ToString()).SendAsync(moveResponse.ToString());
                    if(moveResponse == GameStatus.WhiteCheckMate || moveResponse == GameStatus.BlackCheckMate || moveResponse == GameStatus.Pat)
                    {
                        await _hubLobby.RemoveGame(player.GameId.ToString());
                        var player1 = game.Player1;
                        var player2 = game.Player2;
                        await _hubLobby.RemovePlayer(player1.ConntectionId);
                        await _hubLobby.RemovePlayer(player2.ConntectionId);
                    }
                    await Task.CompletedTask;
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    await Clients.Caller.SendAsync(ex.Message);
                    await Task.CompletedTask;
                }
            }
            await Task.CompletedTask;
        }

        public async Task GetGame()
        {
            var player = _hubLobby.GetPlayer(Context.ConnectionId);
            var game = _gameService.GetGameByGameId(player.GameId.ToString());
            if(game is null)
            {
                await Clients.Caller.SendAsync("Nie jestes w trakcie gry!");
                return;
            }
            await Clients.Caller.SendAsync("Status planszy", game);
        }

        public async Task Surrender()
        {
            var player = _hubLobby.GetPlayer(Context.ConnectionId);
            var game = _hubLobby.GetGame(player.GameId.ToString());
            if (game.Player1 == player)
                await Clients.Group(game.Id.ToString()).SendAsync($"{game.Player2.Name} Win!");
            else
                await Clients.Group(game.Id.ToString()).SendAsync($"{game.Player1.Name} Win!");
            await _hubLobby.RemoveGame(player.GameId.ToString());
            var player1 = game.Player1;
            var player2 = game.Player2;
            await _hubLobby.RemovePlayer(player1.ConntectionId);
            await _hubLobby.RemovePlayer(player2.ConntectionId);
        }
    }
}
