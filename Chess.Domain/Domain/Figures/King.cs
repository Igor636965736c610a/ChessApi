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
        public King(Value value, bool color, Vector2 vector2) : base(value, color, vector2)
        {
            FigureType = FigureType.King;
        }

        public override bool ChcekLegalMovement(Board board, Vector2 newVector2, List<Figure> enemyFigures)
        {
            if (!ChechDirectionValid(newVector2))
                return false;
            foreach (var f in enemyFigures)
            {
                if (f.ChcekLegalMovement(board, newVector2, new List<Figure>()))
                    return false;
            }
            var direction = new Vector2(newVector2.X - Vector2.X, newVector2.Y - Vector2.Y);

            return UtilsMethods.LegalMovement(board.FieldsStatus, Vector2, newVector2, direction, WhiteColor);
        }

        public override bool[,] ShowLegalMovement(Board board, List<Figure> attackingFigures)
        {
            var CanOccupied = new bool[board.XMax, board.YMax];
            for (int i = 0; i < Dirs.Length; i++)
            {
                var direction = new Vector2(Vector2.X + Dirs[i].X, Vector2.Y + Dirs[i].Y);
                bool checkedField = false;
                foreach (var f in attackingFigures)
                {
                    if(f.ChcekLegalMovement(board, direction, new List<Figure>()))
                        checkedField = true;
                }
                if (checkedField == false 
                    && UtilsMethods.LegalMovement(board.FieldsStatus, Vector2, direction, direction, WhiteColor))
                    CanOccupied[Vector2.X + Dirs[i].X, Vector2.Y + Dirs[i].Y] = true;
            }
            return CanOccupied;
        }

        private bool ChechDirectionValid(Vector2 newVector2)
            => !(Vector2.X - newVector2.X > 1 || Vector2.Y - newVector2.Y > 1);

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
    }
}
