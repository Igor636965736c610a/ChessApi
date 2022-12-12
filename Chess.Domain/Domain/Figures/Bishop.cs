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
    public class Bishop : Figure
    {
        public Bishop(Value value, bool color, Vector2 vector2) : base(value, color, vector2)
        {
            FigureType = FigureType.Bishop;
        }

        public override bool ChcekLegalMovement(Board board, Vector2 newVector2, List<Figure> enemyFigures)
        {
            var king = UtilsMethods.GetKing(board.Figures, WhiteColor);
            var fieldsStatus = board.FieldsStatus;
            if (!CheckDirectionValid(newVector2))
                return false;
            if (!UtilsMethods.CheckRevealAttack(Vector2, king.Vector2, board, enemyFigures))
                return false;
            var direction = new Vector2(newVector2.X - Vector2.X, newVector2.Y - Vector2.Y);

            return UtilsMethods.LegalMovement(fieldsStatus, Vector2, newVector2, direction, WhiteColor);
        }
        //public override void SetAttackFields(FieldsStatus[,] fieldsStatus, bool[,] newAttackedFields)
        //{
        //    UtilsMethods.AttackFields(fieldsStatus, Vector2, Dir1, newAttackedFields);
        //    UtilsMethods.AttackFields(fieldsStatus, Vector2, Dir2, newAttackedFields);
        //    UtilsMethods.AttackFields(fieldsStatus, Vector2, Dir3, newAttackedFields);
        //    UtilsMethods.AttackFields(fieldsStatus, Vector2, Dir4, newAttackedFields);
        //}
        private bool CheckDirectionValid(Vector2 newVector2)
            => Math.Abs(Vector2.X - newVector2.X) == Math.Abs(Vector2.Y - newVector2.Y);

        public override bool[,] ShowLegalMovement(Board board, List<Figure> attackingFigures)
        {
            throw new NotImplementedException();
        }

        public override Vector2[] GetDirs()
        {
            throw new NotImplementedException();
        }

        private readonly Vector2 Dir1 = new Vector2(1, 1);
        private readonly Vector2 Dir2 = new Vector2(-1, -1);
        private readonly Vector2 Dir3 = new Vector2(-1, 1);
        private readonly Vector2 Dir4 = new Vector2(1, -1);
    }
}
