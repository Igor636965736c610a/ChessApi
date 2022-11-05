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
        Figure? GetFigure(Board board, Vector2 vector2);
        Figure GetKing(Board board, bool color);
        void RemoveFigure(Board board, Figure figure);
        IEnumerable<Figure> GetFiguresByColor(Board board, bool figureColor);
    }
}
