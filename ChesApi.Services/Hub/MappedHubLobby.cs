using AutoMapper;
using ChesApi.Infrastructure.DTO;
using Chess.Core.Domain;
using Chess.Core.Domain.Figures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChesApi.Infrastructure.Hub
{
    public class MappedHubLobby : IMappedHubLobby
    {
        private readonly IMapper _mapper;
        private readonly IHubLobby _hubLobby;
        public MappedHubLobby(IMapper mapper, IHubLobby hubLobby)
        {
            _hubLobby = hubLobby;
            _mapper = mapper;
        }
        public LiveGameDTO GetGameByGameIdDTO(string gameId)
        {
            var game = _hubLobby.GetGame(gameId);
            return _mapper.Map<LiveGame, LiveGameDTO>(game);
        }

        public PlayerDTO GetPlayerDTO(string connectionId)
        {
            var player = _hubLobby.GetPlayer(connectionId);
            return _mapper.Map<Player, PlayerDTO>(player);
        }

        public List<WaitingPlayersDTO> GetWaitingPlayersDTO()
        {
            var players = _hubLobby.GetWaitingPlayers();
            return _mapper.Map<List<Player>, List<WaitingPlayersDTO>>(players);
        }

        public BoardDTO GetBoardDTO(string gameId)
        {
            var game = _hubLobby.GetGame(gameId);
            return _mapper.Map<Board, BoardDTO>(game.Board);
        }
    }
}
