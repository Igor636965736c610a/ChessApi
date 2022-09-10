using ChesApi.Infrastructure.Services.EnumFiguresDirection;
using Chess.Core.Domain.Enums;
using Chess.Core.Domain.EnumsAndStructs;
using Chess.Core.Domain.static_methods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Core.Domain.Figures
{
    public class Bishop : Figure
    {
        public Bishop(Value value, FigureColor color, Vector2 vector2, Guid id) : base(value, color, vector2, id)
        {
            FigureType = FigureType.Bishop;
        }
        public override void SetAttackFields(FieldsStatus[,] fieldsStatus, bool[,] newAttackedFields)
        {
            StaticMoveLogicMethods.UpRightSetAttackFields(fieldsStatus, newAttackedFields, Vector2);
            StaticMoveLogicMethods.UpLeftSetAttackFields(fieldsStatus, newAttackedFields, Vector2);
            StaticMoveLogicMethods.DownRightSetAttackFields(fieldsStatus, newAttackedFields, Vector2);
            StaticMoveLogicMethods.DownLeftSetAttackFields(fieldsStatus, newAttackedFields, Vector2);
        }
        public override EnumDirection SetDirection(Vector2 newVector2)
        {
            if (Vector2.X > newVector2.X && Vector2.Y < newVector2.Y)
                return EnumDirection.UpLeft;
            if (Vector2.X < newVector2.X && Vector2.Y < newVector2.Y)
                return EnumDirection.UpRight;
            if (Vector2.X > newVector2.X && Vector2.Y > newVector2.Y)
                return EnumDirection.DownLeft;
            if (Vector2.X < newVector2.X && Vector2.Y > newVector2.Y)
                return EnumDirection.DownRight;

            throw new InvalidOperationException();
        }
        public override bool ChcekLegalMovement(FieldsStatus[,] fieldsStatus, Vector2 newVector2, EnumDirection enumDirection)
           => enumDirection switch
        {
            EnumDirection.UpLeft => StaticMoveLogicMethods.UpLeftMovement(Vector2, newVector2, fieldsStatus),
            EnumDirection.UpRight => StaticMoveLogicMethods.UpRightMovement(Vector2, newVector2, fieldsStatus),
            EnumDirection.DownLeft => StaticMoveLogicMethods.DownLeftMovement(Vector2, newVector2, fieldsStatus),
            EnumDirection.DownRight => StaticMoveLogicMethods.DownRightMovement(Vector2, newVector2, fieldsStatus),
            _ => throw new InvalidOperationException(),
        };
        public override bool CheckLegalMoveDirection(Vector2 newVector2)
            => (Math.Abs(Vector2.X - newVector2.X) == Math.Abs(Vector2.Y - newVector2.Y));

        public override bool CheckCheckamte(Vector2 newVector2, IEnumerable<Figure> defendingFigures,
            IEnumerable<Figure> attackingFigures, FieldsStatus[,] fieldsStatus, Vector2 kingVector2, EnumDirection direction)
            => direction switch
        {
            EnumDirection.UpLeft => StaticMoveLogicMethods.UpLeftAttack(Vector2, newVector2, defendingFigures, attackingFigures, fieldsStatus, kingVector2),
            EnumDirection.UpRight => StaticMoveLogicMethods.UpRightAttack(Vector2, newVector2, defendingFigures, attackingFigures, fieldsStatus, kingVector2),
            EnumDirection.DownLeft => StaticMoveLogicMethods.DownLeftAttack(Vector2, newVector2, defendingFigures, attackingFigures, fieldsStatus, kingVector2),
            EnumDirection.DownRight => StaticMoveLogicMethods.DownRightAttack(Vector2, newVector2, defendingFigures, attackingFigures, fieldsStatus, kingVector2),
            _ => throw new InvalidOperationException(),
        };
    }
}
