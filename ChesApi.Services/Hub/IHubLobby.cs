using Chess.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChesApi.Infrastructure.Hub
{
    public interface IHubLobby
    {
        public Task AddPlayer(string connectionId, Player player);
        public Task AddGame(string gameId, LiveGame liveGame);
        public Task AddWaitingPlayer(Player player);
        public Player GetPlayer(string connectionId);
        public List<Player> GetWaitingPlayers();
        public LiveGame GetGame(string gameId);
        public Task RemovePlayer(string connectionId);
        public Task RemoveWaitingPlayer(Player player);
        public Task RemoveGame(string connectionId);
    }
}
