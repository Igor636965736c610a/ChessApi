using Chess.Core.Domain;
using Chess.Core.Domain.Enums;
using Chess.Core.Domain.EnumsAndStructs;
using Chess.Core.Domain.Figures;
using Chess.Core.Repo.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChesApi.Infrastructure.Repo
{
    public class FigureRepository : IFigureRepository
    {
        public Figure? GetFigure(Board board, Vector2 vector2)
            => board.FieldsStatus[vector2.X, vector2.Y];

        public Figure GetKing(Board board, bool color)
            => board.Figures.First(x => x.FigureType == FigureType.King && x.WhiteColor == color);
 
        public void RemoveFigure(Board board, Figure figure)
            => board.Figures.Remove(figure);

        public IEnumerable<Figure> GetFiguresByColor(Board board, bool figureColor)
            => board.Figures.Where(x => x.WhiteColor == figureColor);
    }
}
