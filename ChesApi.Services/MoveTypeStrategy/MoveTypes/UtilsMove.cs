using Chess.Core.Domain;
using Chess.Core.Domain.Enums;
using Chess.Core.Domain.EnumsAndStructs;
using Chess.Core.Domain.Figures;
using Chess.Core.Domain.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChesApi.Infrastructure.MoveTypeStrategy.MoveTypes
{
    internal static class UtilsMove
    {
        public static bool BaseMove(Vector2 newVector2, Figure figure, Board board)
        {
            var enemyFigures = board.Figures
                .Where(x => x.WhiteColor == !figure.WhiteColor && !UtilsMethods.CompareVector2(x.Vector2, newVector2)
                && x.FigureType != FigureType.King);
            var king = board.Figures.First(x => x.FigureType == FigureType.King && x.WhiteColor == figure.WhiteColor);
            if (!figure.CheckLegalMovement(board, board.FieldsStatus, newVector2, enemyFigures, king))
                return false;
            figure.SetNewPosition(newVector2, board);
            return true;
        }
        public static void Promotion(Vector2 newVector2, Figure figure, Board board, Figure newFigure)
        {
            board.FieldsStatus[newVector2.X, newVector2.Y] = newFigure;
            board.Figures.Remove(figure);
            board.Figures.Add(newFigure);
        }
    }
}
