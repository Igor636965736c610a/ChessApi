using Chess.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Core.Repo.Game
{
    public interface IGameRepository
    {
        void CreategGame(User userHost);
        LiveGame GetLiveGame(Guid id);
        Figure GetFigure(LiveGame liveGame, Guid figureId);
    }
}
