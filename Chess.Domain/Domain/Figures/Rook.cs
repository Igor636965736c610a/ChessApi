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
    public class Rook : Figure
    {
        public bool FirstMove { get; private set; } = true;
        public Rook(bool color, Vector2 vector2) : base(color, vector2)
        {
            FigureType = FigureType.Rook;
            Value = 5;
            FigureChar = 'r';
        }

        public override bool ChcekLegalMovement(Board board, Figure?[,] fieldsStatus, Vector2 newVector2, IEnumerable<Figure> enemyFigures, Figure? king)
        {
            if (!CheckDirectionValid(newVector2))
                return false;
            if (king is not null && !UtilsMethods.CheckRevealAttack(Vector2, newVector2, king.Vector2, board, enemyFigures))
                return false;
            var direction = new Vector2(newVector2.X - Vector2.X, newVector2.Y - Vector2.Y);

            return UtilsMethods.LegalMovement(fieldsStatus, Vector2, newVector2, direction, WhiteColor);
        }
        public override void SetNewPosition(Vector2 newVector2, Board board)
        {
            UtilsMethods.SetNewPosition(newVector2, board, this);
            FirstMove = false;
        }
        private bool CheckDirectionValid(Vector2 newVector2)
            => (Vector2.X != newVector2.X && Vector2.Y == newVector2.Y) || (Vector2.Y != newVector2.Y && Vector2.X == newVector2.X);

        public override bool[,] ShowLegalMovement(Board board, List<Figure> attackingFigures)
        {
            throw new NotImplementedException();
        }

        public override Vector2[] GetDirs()
        {
            throw new NotImplementedException();
        }

        private readonly Vector2 Dir1 = new Vector2(1, 0);
        private readonly Vector2 Dir2 = new Vector2(-1, 0);
        private readonly Vector2 Dir3 = new Vector2(0, 1);
        private readonly Vector2 Dir4 = new Vector2(0, -1);
    }
}
