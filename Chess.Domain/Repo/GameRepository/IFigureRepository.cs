using Chess.Core.Domain;
using Chess.Core.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Core.Repo.Game
{
    public interface IFigureRepository
    {
        Figure? GetFigure(LiveGame liveGame, Guid figureId);
        Figure? GetFigure(LiveGame liveGame, int y, int x);
        Figure GetKing(LiveGame liveGmae, FigureColor color);
        void RemoveFigure(LiveGame liveGame, Figure figure);
        IEnumerable<Figure> GetFiguresIsAttacking(LiveGame liveGame, FigureColor figureColor);
        IEnumerable<Figure> GetFiguresByColor(LiveGame liveGame, FigureColor figureColor);
    }
}
