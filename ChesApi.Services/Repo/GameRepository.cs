using Chess.Core.Domain;
using Chess.Core.Domain.DefaultConst;
using Chess.Core.Domain.Enums;
using Chess.Core.Domain.EnumsAndStructs;
using Chess.Core.Repo.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChesApi.Infrastructure.Repo
{
    public class GameRepository : IGameRepository
    {
        public List<LiveGame> _Game = new List<LiveGame>();

        public void CreategGame(User userHost)
        {
            LiveGame liveGame = new LiveGame(new FielsStatus(), userHost); // toDo later
            _Game.Add(liveGame);
            userHost.LiveGame = liveGame;
        }

        public LiveGame GetLiveGame(Guid id)
            => _Game.FirstOrDefault(x => x.Id == id);
    }
}
