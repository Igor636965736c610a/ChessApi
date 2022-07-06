using Chess.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Core.Repo.Game
{
    public interface IFigureRepository
    {
        public Figure GetFigure(LiveGame liveGame, Guid figureId);
        public Figure GetFigure(LiveGame liveGame, int x, int y);
        public void RemoveFigure(LiveGame liveGame, Figure figure);
    }
}
