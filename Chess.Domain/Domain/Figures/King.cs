using Chess.Core.Domain.Enums;
using Chess.Core.Domain.EnumsAndStructs;
using Chess.Core.Domain.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Core.Domain.Figures
{
    public class King : Figure
    {
        public King(bool color, Vector2 vector2) : base(color, vector2)
        {
            FigureType = FigureType.King;
            Value = 0;
            FigureChar = 'k';
        }

        public override bool ChcekLegalMovement(Board board, Figure?[,] fieldsStatus, Vector2 newVector2, IEnumerable<Figure> enemyFigures, Figure? king)
        {
            if (!ChechDirectionValid(newVector2) && !CheckCanCastle(newVector2, board, enemyFigures))
                return false;
            var direction = new Vector2(newVector2.X - Vector2.X, newVector2.Y - Vector2.Y);
            var attackingFieldFigures = enemyFigures.Where(x => !UtilsMethods.CompareVector2(x.Vector2, newVector2));
            var copy2dArray = UtilsMethods.Copy2dArray(fieldsStatus);
            copy2dArray[newVector2.X, newVector2.Y] = null;
            return LegalMovement(board, copy2dArray, newVector2, direction, WhiteColor, attackingFieldFigures);
        }

        public override void SetNewPosition(Vector2 newVector2, Board board)
        {
            if (Math.Abs(Vector2.X - newVector2.X) == 2)
            {
                var kingDirectionMove = Math.Sign(Vector2.X - newVector2.X);
                var rook = board.Figures.First(x => x.WhiteColor == WhiteColor && x.FigureType == FigureType.Rook
                && kingDirectionMove + Math.Sign(x.Vector2.X - Vector2.X) == 0);
                var castelingRookSite = new Vector2(newVector2.X + kingDirectionMove, newVector2.Y);
                UtilsMethods.SetNewPosition(castelingRookSite, board, rook);
            }
            UtilsMethods.SetNewPosition(newVector2, board, this);
        }

        public override bool[,] ShowLegalMovement(Board board, List<Figure> attackingFigures)
        {
            var CanOccupied = new bool[board.XMax, board.YMax];
            for (int i = 0; i < Dirs.Length; i++)
            {
                var direction = new Vector2(Vector2.X + Dirs[i].X, Vector2.Y + Dirs[i].Y);
                bool checkedField = false;
                if (attackingFigures.Any(x => x.ChcekLegalMovement(board, board.FieldsStatus, direction, new List<Figure>(), null)))
                    checkedField = true;
                if (!checkedField && UtilsMethods.LegalMovement(board.FieldsStatus, Vector2, direction, direction, WhiteColor))
                    CanOccupied[Vector2.X + Dirs[i].X, Vector2.Y + Dirs[i].Y] = true;
            }
            return CanOccupied;
        }

        private bool ChechDirectionValid(Vector2 newVector2)
            => !(Math.Abs(Vector2.X - newVector2.X) > 1 || Math.Abs(Vector2.Y - newVector2.Y) > 1);

        public override Vector2[] GetDirs()
            => Dirs;

        private readonly Vector2[] Dirs = 
        { 
            new Vector2(1, 0), 
            new Vector2(-1, 0), 
            new Vector2(0, 1), 
            new Vector2(0, -1),
            new Vector2(1, 1),
            new Vector2(-1, -1),
            new Vector2(-1, 1),
            new Vector2(1, -1)
        };

        private bool CheckCanCastle(Vector2 newVector2, Board board, IEnumerable<Figure> enemyFigures)
            => Vector2.Y - newVector2.Y == 0 && Math.Abs(Vector2.X - newVector2.X) == 2
            && !enemyFigures.Any(x => x.ChcekLegalMovement(board, board.FieldsStatus, Vector2, new List<Figure>(), null))
            && board.Figures.Where(x => x.FigureType == FigureType.Rook && x.WhiteColor == WhiteColor).Cast<Rook>().Any(x => x.FirstMove
            && Math.Sign(Vector2.X - newVector2.X) + Math.Sign(x.Vector2.X - Vector2.X) == 0);

        private bool LegalMovement(Board board, Figure?[,] fieldsStatus, Vector2 newVector2, Vector2 direction,
            bool color, IEnumerable<Figure> attackingFigures)
        {
            var step = new Vector2(Math.Sign(direction.X), Math.Sign(direction.Y));
            var current = Vector2;

            while (!UtilsMethods.CompareVector2(current, newVector2))
            {
                current.X += step.X;
                current.Y += step.Y;

                if (!UtilsMethods.CheckOccupied(fieldsStatus, current) && !UtilsMethods.CompareVector2(current, newVector2))
                    return false;
                if (attackingFigures.Any(x => x.ChcekLegalMovement(board, fieldsStatus, newVector2, new List<Figure>(), null)))
                    return false;
            }
            return !(fieldsStatus[newVector2.X, newVector2.Y]?.WhiteColor == color);
        }
    }
}
