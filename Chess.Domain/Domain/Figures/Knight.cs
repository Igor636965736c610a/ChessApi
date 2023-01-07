using Chess.Core.Domain.Enums;
using Chess.Core.Domain.EnumsAndStructs;
using Chess.Core.Domain.Utils;

namespace Chess.Core.Domain.Figures
{
    public class Knight : Figure
    {
        public Knight(bool color, Vector2 vector2) : base(color, vector2)
        {
            FigureType = FigureType.Knight;
            Value = 3;
            FigureChar = 'n';
        }

        public override bool CheckLegalMovement(Board board, Figure?[,] fieldsStatus, Vector2 newVector2, IEnumerable<Figure> enemyFigures, Figure? king)
        {
            if (!CheckDirectionValid(newVector2))
                return false;
            if (king is not null && !UtilsMethods.CheckRevealAttack(Vector2, newVector2, king.Vector2, board, enemyFigures))
                return false;
            return !(fieldsStatus[newVector2.X, newVector2.Y]?.WhiteColor == WhiteColor);
        }

        private bool CheckDirectionValid(Vector2 newVector2)
            => Math.Abs(Vector2.X - newVector2.X) * Math.Abs(Vector2.Y - newVector2.Y) == 2;

        public override IEnumerable<Vector2> Dirs { get; } = new List<Vector2>()
        {
            new Vector2(2, 1),
            new Vector2(2, -1),
            new Vector2(1, 2),
            new Vector2(1, -2),
            new Vector2(-2, 1),
            new Vector2(-2, -1),
            new Vector2(-1, 2),
            new Vector2(-1, -2)
        };

        public override IEnumerable<Vector2> ShowLegalMovement(Board board, IEnumerable<Figure> attackingFigures)
        {
            throw new NotImplementedException();
        }
    }
}
