using Chess.Core.Domain.Enums;
using Chess.Core.Domain.EnumsAndStructs;

namespace Chess.Core.Domain.Figures
{
    public class Knight : Figure
    {
        public Knight(Value value, bool color, Vector2 vector2, Guid id) : base(value, color, vector2, id)
        {
            FigureType = FigureType.Knight;
        }

        public override bool ChcekLegalMovement(LiveGame liveGame, Vector2 newVector2)
        {
            var fieldsStatus = liveGame.FieldsStatus;
            if (!CheckDirectionValid(newVector2))
                return false;
            return !(fieldsStatus[newVector2.X, newVector2.Y].Figure?.WhiteColor == WhiteColor);
        }
        public override void SetAttackFields(FieldsStatus[,] fieldsStatus, bool[,] newAttackedFields)
        {
            AttackFields(Vector2, Dir1, newAttackedFields);
            AttackFields(Vector2, Dir2, newAttackedFields);
            AttackFields(Vector2, Dir3, newAttackedFields);
            AttackFields(Vector2, Dir4, newAttackedFields);
            AttackFields(Vector2, Dir5, newAttackedFields);
            AttackFields(Vector2, Dir6, newAttackedFields);
            AttackFields(Vector2, Dir7, newAttackedFields);
            AttackFields(Vector2, Dir8, newAttackedFields);
        }
        private bool CheckDirectionValid(Vector2 newVector2)
            => Math.Abs(Vector2.X - newVector2.X) * Math.Abs(Vector2.Y - newVector2.Y) == 2;
        private void AttackFields(Vector2 vector2, Vector2 dir, bool[,] newAttakFields)
        {
            vector2.X += vector2.X + dir.X;
            vector2.Y += vector2.Y + dir.Y;
            newAttakFields[vector2.X, vector2.Y] = true;
        }
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
