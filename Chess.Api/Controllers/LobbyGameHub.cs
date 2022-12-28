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
        private readonly IMappedHubLobby _mappedHubLobby;
        public LobbyGameHub(IHubLobby hubLobby, IGameService gameService, IMappedHubLobby mappedHubLobby)
        {
            _hubLobby = hubLobby;
            _gameService = gameService;
            _mappedHubLobby = mappedHubLobby;
        }
        public override Task OnConnectedAsync()
        {
            return Clients.Caller.SendAsync("onConnected", "Witaj", _mappedHubLobby.GetWaitingPlayersDTO());
        }
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var leavingPlayer = _hubLobby.GetPlayer(Context.ConnectionId);
            if(_hubLobby.GetGame(leavingPlayer.GameId.ToString()) is not null)
            {
                if (leavingPlayer.WhiteColor)
                    await Clients.Group(leavingPlayer.GameId.ToString()).SendAsync("disconected", "White Player Win");
                else
                    await Clients.Group(leavingPlayer.GameId.ToString()).SendAsync("disconected", "Black Player Win");
                var game = _hubLobby.GetGame(leavingPlayer.GameId.ToString());
                await _hubLobby.RemovePlayer(game.Player1.ConntectionId);
                await _hubLobby.RemovePlayer(game.Player2.ConntectionId);
                await _hubLobby.RemoveGame(leavingPlayer.GameId.ToString());
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
                await Clients.Caller.SendAsync("NoGame", "Nie ma takiej gry!");
                return;
            }
            var player2 = Factory.GetPlayer(userId, name, Context.ConnectionId, false, false);
            var game = Factory.GetGame(player1, player2);
            await _hubLobby.RemoveWaitingPlayer(player1);
            await _hubLobby.AddGame(game.Id.ToString(), game);
            await _hubLobby.AddPlayer(Context.ConnectionId, player2);
            await Groups.AddToGroupAsync(game.Player1.ConntectionId, groupName: game.Id.ToString());
            await Groups.AddToGroupAsync(game.Player2.ConntectionId, groupName: game.Id.ToString());
            await Clients.Group(game.Id.ToString()).SendAsync("StartGame", "Start");
        }

        public async Task Move(Vector2 current, Vector2 target, MoveType moveType)
        {
            var player = _hubLobby.GetPlayer(Context.ConnectionId);
            var game = _hubLobby.GetGame(player.GameId.ToString());
            if (player.HasMove)
            {
                try
                {
                    var moveResponse = _gameService.Move(moveType, target, current, game);
                    game.Player1.HasMove = !game.Player1.HasMove;
                    game.Player2.HasMove = !game.Player2.HasMove;
                    game.WhiteColor = !game.WhiteColor;
                    var boardDTO = _mappedHubLobby.GetBoardDTO(player.GameId.ToString());
                    await Clients.Group(game.Id.ToString()).SendAsync("MoveResponse", moveResponse.ToString(), boardDTO);
                    if(moveResponse == GameStatus.WhiteCheckMate || moveResponse == GameStatus.BlackCheckMate || moveResponse == GameStatus.Pat)
                    {
                        await _hubLobby.RemovePlayer(game.Player1.ConntectionId);
                        await _hubLobby.RemovePlayer(game.Player2.ConntectionId);
                        await _hubLobby.RemoveGame(player.GameId.ToString());
                    }
                    await Task.CompletedTask;
                }
                catch(Exception ex)
                {
                    await Clients.Caller.SendAsync("ErrorMove", ex.Message);
                    await Task.CompletedTask;
                }
            }
            await Task.CompletedTask;
        }

        public async Task GetGameStatus()
        {
            var player = _hubLobby.GetPlayer(Context.ConnectionId);
            var game = _mappedHubLobby.GetGameByGameIdDTO(player.GameId.ToString());
            if(game is null)
            {
                await Clients.Caller.SendAsync("NotFoundGame", "Nie jestes w trakcie gry!");
                return;
            }
            await Clients.Caller.SendAsync("BoardStatus", game);
        }

        public async Task Surrender()
        {
            var player = _hubLobby.GetPlayer(Context.ConnectionId);
            var game = _hubLobby.GetGame(player.GameId.ToString());
            if (game.Player1 == player)
                await Clients.Group(game.Id.ToString()).SendAsync("WinAfterSurrender", $"{game.Player2.Name} Win!");
            else
                await Clients.Group(game.Id.ToString()).SendAsync("WinAfterSurrender", $"{game.Player1.Name} Win!");
            await _hubLobby.RemovePlayer(game.Player1.ConntectionId);
            await _hubLobby.RemovePlayer(game.Player2.ConntectionId);
            await _hubLobby.RemoveGame(player.GameId.ToString());
        }
    }
}
