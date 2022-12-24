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
        //public override void SetAttackFields(FieldsStatus[,] fieldsStatus, bool[,] newAttackedFields)
        //{
        //    UtilsMethods.AttackFields(fieldsStatus, Vector2, Dir1, newAttackedFields);
        //    UtilsMethods.AttackFields(fieldsStatus, Vector2, Dir2, newAttackedFields);
        //    UtilsMethods.AttackFields(fieldsStatus, Vector2, Dir3, newAttackedFields);
        //    UtilsMethods.AttackFields(fieldsStatus, Vector2, Dir4, newAttackedFields);
        //    UtilsMethods.AttackFields(fieldsStatus, Vector2, Dir5, newAttackedFields);
        //    UtilsMethods.AttackFields(fieldsStatus, Vector2, Dir6, newAttackedFields);
        //    UtilsMethods.AttackFields(fieldsStatus, Vector2, Dir7, newAttackedFields);
        //    UtilsMethods.AttackFields(fieldsStatus, Vector2, Dir8, newAttackedFields);
        //}
        public override bool ChcekLegalMovement(Board board, Vector2 newVector2, List<Figure> enemyFigures, Figure? king)
        {
            if (!CheckDirectionValid(newVector2))
                return false;
            if (king is not null && !UtilsMethods.CheckRevealAttack(Vector2, king.Vector2, board, enemyFigures))
                return false;
            var direction = new Vector2(newVector2.X - Vector2.X, newVector2.Y - Vector2.Y);

            return UtilsMethods.LegalMovement(board.FieldsStatus, Vector2, newVector2, direction, WhiteColor);
        }
        private bool CheckDirectionValid(Vector2 newVector2)
            => (Vector2.X != newVector2.X && Vector2.Y == newVector2.Y) || (Vector2.Y != newVector2.Y && Vector2.X == newVector2.X) || (Math.Abs(Vector2.X - newVector2.X) == Math.Abs(Vector2.Y - newVector2.Y));

        public override bool[,] ShowLegalMovement(Board board, List<Figure> attackingFigures)
        {
            throw new NotImplementedException();
        }

        public override Vector2[] GetDirs()
        {
            throw new NotImplementedException();
        }

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
