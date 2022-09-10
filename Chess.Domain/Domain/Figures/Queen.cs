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
    public class Queen : Figure
    {
        public Queen(Value value, FigureColor color, Vector2 vector2, Guid id) : base(value, color, vector2, id)
        {
            FigureType = FigureType.Queen;
        }
        public override void SetAttackFields(FieldsStatus[,] fieldsStatus, bool[,] newAttackedFields)
        {
            StaticMoveLogicMethods.UpSetAttackFields(fieldsStatus, newAttackedFields, Vector2);
            StaticMoveLogicMethods.DownSetAttackFields(fieldsStatus, newAttackedFields, Vector2);
            StaticMoveLogicMethods.LeftSetAttackFields(fieldsStatus, newAttackedFields, Vector2);
            StaticMoveLogicMethods.RightSetAttackFields(fieldsStatus, newAttackedFields, Vector2);
            StaticMoveLogicMethods.UpRightSetAttackFields(fieldsStatus, newAttackedFields, Vector2);
            StaticMoveLogicMethods.UpLeftSetAttackFields(fieldsStatus, newAttackedFields, Vector2);
            StaticMoveLogicMethods.DownRightSetAttackFields(fieldsStatus, newAttackedFields, Vector2);
            StaticMoveLogicMethods.DownLeftSetAttackFields(fieldsStatus, newAttackedFields, Vector2);
        }
        public override EnumDirection SetDirection(Vector2 newVector2)
        {
            if (Vector2.Y > newVector2.Y && Vector2.X == newVector2.X)
                return EnumDirection.Down;
            if (Vector2.Y < newVector2.Y && Vector2.X == newVector2.X)
                return EnumDirection.Up;
            if (Vector2.X < newVector2.X && Vector2.Y == newVector2.Y)
                return EnumDirection.Right;
            if (Vector2.X > newVector2.X && Vector2.Y == newVector2.Y)
                return EnumDirection.Left;
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
            EnumDirection.Up => StaticMoveLogicMethods.UpMovement(Vector2, newVector2, fieldsStatus),
            EnumDirection.Down => StaticMoveLogicMethods.DownMovement(Vector2, newVector2, fieldsStatus),
            EnumDirection.Left => StaticMoveLogicMethods.LeftMovement(Vector2, newVector2, fieldsStatus),
            EnumDirection.Right => StaticMoveLogicMethods.RightMovement(Vector2, newVector2, fieldsStatus),
            EnumDirection.UpLeft => StaticMoveLogicMethods.UpLeftMovement(Vector2, newVector2, fieldsStatus),
            EnumDirection.UpRight => StaticMoveLogicMethods.UpRightMovement(Vector2, newVector2, fieldsStatus),
            EnumDirection.DownLeft => StaticMoveLogicMethods.DownLeftMovement(Vector2, newVector2, fieldsStatus),
            EnumDirection.DownRight => StaticMoveLogicMethods.DownRightMovement(Vector2, newVector2, fieldsStatus),
            _ => throw new InvalidOperationException(),
        };
        public override bool CheckLegalMoveDirection(Vector2 newVector2)
            => (Vector2.X != newVector2.X && Vector2.Y == newVector2.Y || Vector2.Y != newVector2.Y && Vector2.X == newVector2.X || Math.Abs(Vector2.X - newVector2.X) == Math.Abs(Vector2.Y - newVector2.Y));

        public override bool CheckCheckamte(Vector2 newVector2, IEnumerable<Figure> defendingFigures,
            IEnumerable<Figure> attackingFigures, FieldsStatus[,] fieldsStatus, Vector2 kingVector2, EnumDirection direction)
        {
            throw new InvalidOperationException();
        }
    }
}
