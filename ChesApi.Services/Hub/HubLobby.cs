using Chess.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChesApi.Infrastructure.Hub
{
    public class HubLobby : IHubLobby
    {
        private readonly Dictionary<string, Player> Players = new();
        private readonly Dictionary<string, LiveGame> Games = new();
        private readonly List<Player> WaitingPlayers = new();

        public Task AddPlayer(string connectionId, Player player)
        {
            Players.Add(connectionId, player);
            return Task.CompletedTask;
        }
        public Task AddGame(string connectionId, LiveGame liveGame)
        {
            Games.Add(connectionId, liveGame);
            return Task.CompletedTask;
        }
        public Task AddWaitingPlayer(Player player)
        {
            WaitingPlayers.Add(player);
            return Task.CompletedTask;
        }
        public Player GetPlayer(string connectionId)
            => Players[connectionId];

        public List<Player> GetWaitingPlayers()
            => WaitingPlayers;

        public LiveGame GetGame(string connectionId)
            => Games[connectionId];

        public Task RemovePlayer(string connectionId)
        {
            Players.Remove(connectionId);
            return Task.CompletedTask;
        }
        public Task RemoveWaitingPlayer(Player player)
        {
            WaitingPlayers.Remove(player);
            return Task.CompletedTask;
        }
        public Task RemoveGame(string connectionId)
        {
            Games.Remove(connectionId);
            return Task.CompletedTask;
        }
    }
}

