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
        LiveGameDTO GetGameByGameIdDTO(string gameId);
        PlayerDTO GetPlayerDTO(string connectionId);
        public List<WaitingPlayersDTO> GetWaitingPlayersDTO();
        public BoardDTO GetBoardDTO(string gameId);
    }
}
