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
    public class Queen : Figure
    {
        public Queen(bool color, Vector2 vector2) : base(color, vector2)
        {
            FigureType = FigureType.Queen;
            Value = 8;
            FigureChar = 'q';
        }

        public override bool CheckLegalMovement(Board board, Figure?[,] fieldsStatus, Vector2 newVector2, IEnumerable<Figure> enemyFigures, Figure? king)
        {
            if (!CheckDirectionValid(newVector2))
                return false;
            if (king is not null && !UtilsMethods.CheckRevealAttack(Vector2, newVector2, king.Vector2, board, enemyFigures))
                return false;
            var direction = new Vector2(newVector2.X - Vector2.X, newVector2.Y - Vector2.Y);

            return UtilsMethods.LegalMovement(fieldsStatus, Vector2, newVector2, direction, WhiteColor);
        }
        private bool CheckDirectionValid(Vector2 newVector2)
            => (Vector2.X != newVector2.X && Vector2.Y == newVector2.Y) || (Vector2.Y != newVector2.Y && Vector2.X == newVector2.X) || (Math.Abs(Vector2.X - newVector2.X) == Math.Abs(Vector2.Y - newVector2.Y));

        public override IEnumerable<Vector2> ShowLegalMovement(Board board, IEnumerable<Figure> attackingFigures)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<Vector2> Dirs { get; } = new List<Vector2>()
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
