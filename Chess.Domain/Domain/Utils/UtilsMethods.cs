using Chess.Core.Domain.Enums;
using Chess.Core.Domain.EnumsAndStructs;
using Chess.Core.Domain.Figures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Core.Domain.Utils
{
    public static class UtilsMethods
    {
        public static bool LegalMovement(Figure?[,] fieldsStatus, Vector2 current, Vector2 newVector2, Vector2 direction, 
            bool color)
        {
            var step = new Vector2(Math.Sign(direction.X), Math.Sign(direction.Y));

            while ((current.X != newVector2.X) && (current.Y != newVector2.Y))
            {
                current.X += step.X;
                current.Y += step.Y;

                if (!CheckOccupied(fieldsStatus, current))
                    return false;
            }
            return !(fieldsStatus[newVector2.X, newVector2.Y]?.WhiteColor == color);
        }
        public static bool CheckRevealAttack(Vector2 currentVector2, Vector2 kingVector2, Board board,
            List<Figure> attackingFigures)
        {
            var figure = board.FieldsStatus[currentVector2.X, currentVector2.Y];
            board.FieldsStatus[currentVector2.X, currentVector2.Y] = null;
            var checkLegal = figure.ChcekLegalMovement(board, kingVector2, attackingFigures);
            board.FieldsStatus[figure.Vector2.X, figure.Vector2.Y] = figure;
            return checkLegal;
        }
        public static void ShowLegalMovement(Figure?[,] fieldsStatus, Vector2 current, Vector2 step, Vector2 distance, bool[,] CanOccupied, bool color)
        {
            while ((current.X != distance.X) && (current.Y != distance.Y))
            {
                current.X += step.X;
                current.Y += step.Y;

                if (!CheckOccupied(fieldsStatus, current))
                {
                    if (fieldsStatus[current.X, current.Y].WhiteColor == color)
                    {
                        return;
                    }
                    CanOccupied[current.X, current.Y] = true;
                    return;
                }
                CanOccupied[current.X, current.Y] = true;
            }
        }
        public static bool CheckCover(Vector2 current, IEnumerable<Figure> defendingFigures, List<Figure> attackingFigures,
            Board board)
        {
            foreach (var f in defendingFigures)
            {
                if (f.ChcekLegalMovement(board, current, attackingFigures))
                    return true;
            }
            return false;
        }
        internal static bool CheckOccupied(Figure?[,] fieldsStatus, Vector2 current)
            => fieldsStatus[current.X, current.Y] is null;

        public static Figure GetKing(List<Figure> figures, bool color)
            => figures.First(x => x.FigureType == FigureType.King && x.WhiteColor == color);

        public static void SetNewPosition(Vector2 newVector2, Board board, Figure figure)
        {
            var figureToDelete = board.FieldsStatus[newVector2.X, newVector2.Y];
            if (figureToDelete is not null)
                board.Figures.Remove(figureToDelete);
            board.FieldsStatus[figure.Vector2.X, figure.Vector2.Y] = null;
            board.FieldsStatus[newVector2.X, newVector2.Y] = figure;
            figure.Vector2 = newVector2;
            board.EnPassant = new EnPassant();
        }
    }
}
