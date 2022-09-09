using Chess.Core.Domain;
using Chess.Core.Domain.Enums;
using Chess.Core.Domain.EnumsAndStructs;
using Chess.Core.Domain.Figures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Core.Repo.Game
{
    public interface IFigureRepository
    {
        Figure? GetFigure(LiveGame liveGame, Vector2 vector2);
        Figure GetKing(LiveGame liveGmae, FigureColor color);
        void RemoveFigure(LiveGame liveGame, Figure figure);
        IEnumerable<Figure> GetFiguresIsAttacking(LiveGame liveGame, FigureColor figureColor);
        IEnumerable<Figure> GetFiguresByColor(LiveGame liveGame, FigureColor figureColor);
    }
}
