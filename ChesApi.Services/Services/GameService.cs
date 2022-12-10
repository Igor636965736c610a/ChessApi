using AutoMapper;
using ChesApi.Infrastructure.DTO;
using ChesApi.Infrastructure.Hub;
using Chess.Core.Domain;
using Chess.Core.Repo.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChesApi.Infrastructure.Services
{
    public class GameService : IGameService
    {
        private readonly IMapper _mapper;
        private readonly IHubLobby _hubLobby;
        public GameService(IMapper mapper, IHubLobby hubLobby)
        {
            _mapper = mapper;
            _hubLobby = hubLobby;
        }

        public LiveGameDTO GetGame(string id)
        {
            var game = _hubLobby.GetGame(id);
            return _mapper.Map<LiveGame, LiveGameDTO>(game);
        }

        public PlayerDTO GetPlayer(string connectionId)
        {
            var player = _hubLobby.GetPlayer(connectionId);
            return _mapper.Map<Player, PlayerDTO>(player);
        }
    }
}
