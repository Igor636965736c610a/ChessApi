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
        internal static bool LegalMovement(Figure?[,] fieldsStatus, Vector2 current, Vector2 newVector2, Vector2 direction, 
            bool color)
        {
            var step = new Vector2(Math.Sign(direction.X), Math.Sign(direction.Y));

            while (!CompareVector2(current, newVector2))
            {                
                current.X += step.X;
                current.Y += step.Y;
                
                if (!CheckOccupied(fieldsStatus, current) && !CompareVector2(current, newVector2))
                    return false;
            }
            return !(fieldsStatus[newVector2.X, newVector2.Y]?.WhiteColor == color);
        }
        internal static bool CheckRevealAttack(Vector2 currentVector2, Vector2 newVector2, Vector2 kingVector2, Board board,
            List<Figure> enemyFigures)
        {
            var copiedFieldsStatus = Copy2dArray(board.FieldsStatus);
            var figure = copiedFieldsStatus[currentVector2.X, currentVector2.Y];
            if (figure is null)
                throw new Exception("debil alert");
            copiedFieldsStatus[currentVector2.X, currentVector2.Y] = null;
            copiedFieldsStatus[newVector2.X, newVector2.Y] = figure;
            return !enemyFigures.Any(x => x.ChcekLegalMovement(board, copiedFieldsStatus, kingVector2, new List<Figure>(), null));
        }
        public static bool CheckCover(Vector2 current, IEnumerable<Figure> defendingFigures, List<Figure> attackingFigures,
            Board board, Figure king)
            => defendingFigures.Any(x => x.ChcekLegalMovement(board, board.FieldsStatus, current, attackingFigures, king));

        internal static bool CheckOccupied(Figure?[,] fieldsStatus, Vector2 current)
            => fieldsStatus[current.X, current.Y] is null;

        internal static void SetNewPosition(Vector2 newVector2, Board board, Figure figure)
        {
            var figureToDelete = board.FieldsStatus[newVector2.X, newVector2.Y];
            if (figureToDelete is not null)
                board.Figures.Remove(figureToDelete);
            board.FieldsStatus[figure.Vector2.X, figure.Vector2.Y] = null;
            board.FieldsStatus[newVector2.X, newVector2.Y] = figure;
            figure.Vector2 = newVector2;
            board.EnPassant = new EnPassant();
        }
        internal static Figure?[,] Copy2dArray(Figure?[,] arr)
        {
            Figure?[,] local = new Figure[arr.GetLength(0), arr.GetLength(1)];
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int z = 0; z < arr.GetLength(1); z++)
                {
                    local[i, z] = arr[i, z];
                }
            }
            return local;
        }
        public static bool CompareVector2(Vector2 current, Vector2 newVector2)
            => current.X == newVector2.X && current.Y == newVector2.Y;

        public static bool ValidateVetor2(Vector2 vector2, Board board)
            => vector2.X < board.XMin || vector2.X >= board.XMax || vector2.Y < board.XMin || vector2.Y >= board.YMax;
    }
}
