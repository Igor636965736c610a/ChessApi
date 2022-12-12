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
        }

        public override bool ChcekLegalMovement(Board board, Vector2 newVector2, List<Figure> enemyFigures)
        {
            var king = UtilsMethods.GetKing(board.Figures, WhiteColor);
            var fieldsStatus = board.FieldsStatus;
            if (!CheckDirectionValid(newVector2))
                return false;
            if (!UtilsMethods.CheckRevealAttack(Vector2, king.Vector2, board, enemyFigures))
                return false;
            return !(fieldsStatus[newVector2.X, newVector2.Y]?.WhiteColor == WhiteColor);
        }
        //public override void SetAttackFields(Figure[,] fieldsStatus, bool[,] newAttackedFields)
        //{
        //    AttackFields(Vector2, Dir1, newAttackedFields);
        //    AttackFields(Vector2, Dir2, newAttackedFields);
        //    AttackFields(Vector2, Dir3, newAttackedFields);
        //    AttackFields(Vector2, Dir4, newAttackedFields);
        //    AttackFields(Vector2, Dir5, newAttackedFields);
        //    AttackFields(Vector2, Dir6, newAttackedFields);
        //    AttackFields(Vector2, Dir7, newAttackedFields);
        //    AttackFields(Vector2, Dir8, newAttackedFields);
        //}
        private bool CheckDirectionValid(Vector2 newVector2)
            => Math.Abs(Vector2.X - newVector2.X) * Math.Abs(Vector2.Y - newVector2.Y) == 2;

        public override bool[,] ShowLegalMovement(Board board, List<Figure> attackingFigures)
        {
            throw new NotImplementedException();
        }

        public override Vector2[] GetDirs()
        {
            throw new NotImplementedException();
        }

        //private void AttackFields(Vector2 vector2, Vector2 dir, bool[,] newAttakFields)
        //{
        //    vector2.X += vector2.X + dir.X;
        //    vector2.Y += vector2.Y + dir.Y;
        //    if(vector2.X < 8 && vector2.X >= 0 && vector2.Y < 8 && vector2.Y >= 0)
        //        newAttakFields[vector2.X, vector2.Y] = true;
        //}
        private readonly Vector2 Dir1 = new Vector2(2, 1);
        private readonly Vector2 Dir2 = new Vector2(2, -1);
        private readonly Vector2 Dir3 = new Vector2(1, 2);
        private readonly Vector2 Dir4 = new Vector2(1, -2);
        private readonly Vector2 Dir5 = new Vector2(-2, 1);
        private readonly Vector2 Dir6 = new Vector2(-2, -1);
        private readonly Vector2 Dir7 = new Vector2(-1, 2);
        private readonly Vector2 Dir8 = new Vector2(-1, -2);
    }
}
