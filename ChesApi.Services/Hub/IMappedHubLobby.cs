using ChesApi.Infrastructure.DTO;
using Chess.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChesApi.Infrastructure.Hub
{
    public interface IMappedHubLobby
    {
        LiveGameDTO GetGameByGameId(string gameId);
        PlayerDTO GetPlayer(string connectionId);
        public List<WaitingPlayersDTO> GetWaitingPlayersDTO();
    }
}
