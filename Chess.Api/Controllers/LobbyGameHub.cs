using ChesApi.Infrastructure.Factory;
using ChesApi.Infrastructure.MoveTypeStrategy.Enum;
using ChesApi.Infrastructure.Services;
using Chess.Api.GetClaims;
using Chess.Core.Domain;
using Chess.Core.Domain.EnumsAndStructs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SignalRSwaggerGen.Attributes;
using System.Security.Claims;

namespace Chess.Api.Controllers
{
    [Authorize]
    [SignalRHub]
    public class LobbyGameHub : Hub
    {
        private List<Player> WaitingPlayers = new();
        private List<LiveGame> LiveGames = new();
        private List<Player> PlayerList = new();
        private readonly IMoveService _moveService;
        public LobbyGameHub(IMoveService moveService)
        {
            _moveService = moveService;
        }
        public override Task OnConnectedAsync()
        {
            return Clients.All.SendAsync("cos");
        }
        public async Task CreateRoom()
        {
            var userId = GetClaimsProperty.GetUserId(Context);
            var name = GetClaimsProperty.GetName(Context);
            var player = Factory.GetPlayer(userId, name, Context.ConnectionId, true, true);
            WaitingPlayers.Add(player);
            await Clients.All.SendAsync("AddRoom", player);
        }

        public async Task JoinRoom(string roomId)
        {
            var userId = GetClaimsProperty.GetUserId(Context);
            var name = GetClaimsProperty.GetName(Context);
            var player1 = WaitingPlayers.FirstOrDefault(x => x.ConntectionId == roomId);
            if(player1 == null)
            {
                Console.WriteLine("pupa");
                await Clients.Caller.SendAsync("xxxxx");
                return;
            }
            var player2 = Factory.GetPlayer(userId, name, Context.ConnectionId, false, false);
            var game = Factory.GetGame(player1, player2);
            WaitingPlayers.Remove(player1);
            LiveGames.Add(game);
            PlayerList.Add(player1);
            PlayerList.Add(player2);
            Console.WriteLine("x");
            await Groups.AddToGroupAsync(game.Player1.ConntectionId, groupName: game.Id.ToString());
            Console.WriteLine("a");
            await Groups.AddToGroupAsync(game.Player2.ConntectionId, groupName: game.Id.ToString());
            Console.WriteLine("b");
            await Clients.Group(game.Id.ToString()).SendAsync("Start", game);
            Console.WriteLine("c");
            //await Task.WhenAll(
            //    Groups.AddToGroupAsync(game.Player1.ConntectionId, groupName: game.Id.ToString()),
            //    Groups.AddToGroupAsync(game.Player2.ConntectionId, groupName: game.Id.ToString()),
            //    Clients.Group(game.Id.ToString()).SendAsync("Start", game));
        }

        public async Task Move(Vector2 current, Vector2 target, MoveType moveType)
        {
            var player = PlayerList.First(x => x.ConntectionId == Context.ConnectionId);
            var game = LiveGames.First(x => x.Id == player.GameId);
            if (player.HasMove)
            {
                try
                {
                    var moveResponse = await Task.FromResult(_moveService.Move(moveType, current, target, game));
                    //player.HasMove = !player.HasMove;

                }
                catch(Exception)
                {

                }
            }
        }
    }
}
