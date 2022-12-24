using ChesApi.Infrastructure.DTO;
using ChesApi.Infrastructure.MoveTypeStrategy.Enum;
using Chess.Core.Domain;
using Chess.Core.Domain.EnumsAndStructs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChesApi.Infrastructure.Services
{
    public interface IGameService
    {
        LiveGameDTO GetGameByGameId(string gameId);
        public GameCharDTO[] GetArrayGameRepresentation(LiveGame game);
        PlayerDTO GetPlayer(string connectionId);
        GameStatus Move(MoveType moveType, Vector2 newVector2, Vector2 oldVector2, LiveGame liveGame);
    }
}
