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
    public class Rook : Figure
    {
        public bool FirstMove { get; set; } = true;
        public Rook(Value value, FigureColor color, Vector2 vector2, Guid id) : base(value, color, vector2, id)
        {
            FigureType = FigureType.Rook;
        }

        public override void SetAttackFields(FieldsStatus[,] fieldsStatus, bool[,] newAttackedFields)
        {
            StaticMoveLogicMethods.UpSetAttackFields(fieldsStatus, newAttackedFields, Vector2);
            StaticMoveLogicMethods.DownSetAttackFields(fieldsStatus, newAttackedFields, Vector2);
            StaticMoveLogicMethods.LeftSetAttackFields(fieldsStatus, newAttackedFields, Vector2);
            StaticMoveLogicMethods.RightSetAttackFields(fieldsStatus, newAttackedFields, Vector2);
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

            throw new InvalidOperationException();
        }
        public override bool ChcekLegalMovement(FieldsStatus[,] fieldsStatus, Vector2 newVector2, EnumDirection enumDirection)
             => enumDirection switch
        {
            EnumDirection.Up => StaticMoveLogicMethods.UpMovement(Vector2, newVector2, fieldsStatus),
            EnumDirection.Down => StaticMoveLogicMethods.DownMovement(Vector2, newVector2, fieldsStatus),
            EnumDirection.Left => StaticMoveLogicMethods.LeftMovement(Vector2, newVector2, fieldsStatus),
            EnumDirection.Right => StaticMoveLogicMethods.RightMovement(Vector2, newVector2, fieldsStatus),
            _ => throw new InvalidOperationException(),
        };
        public override bool CheckLegalMoveDirection(Vector2 newVector2)
            => (Vector2.X != newVector2.X && Vector2.Y == newVector2.Y) || (Vector2.Y != newVector2.Y && Vector2.X == newVector2.X);

        public override bool CheckCheckamte(Vector2 newVector2, IEnumerable<Figure> defendingFigures,
            IEnumerable<Figure> attackingFigures, FieldsStatus[,] fieldsStatus, Vector2 kingVector2, EnumDirection direction)
            => direction switch
            {
            EnumDirection.Up => StaticMoveLogicMethods.UpAttack(Vector2, newVector2, defendingFigures, attackingFigures, fieldsStatus, kingVector2),
            EnumDirection.Down => StaticMoveLogicMethods.UpAttack(Vector2, newVector2, defendingFigures, attackingFigures, fieldsStatus, kingVector2),
            EnumDirection.Left => StaticMoveLogicMethods.UpAttack(Vector2, newVector2, defendingFigures, attackingFigures, fieldsStatus, kingVector2),
            EnumDirection.Right => StaticMoveLogicMethods.UpAttack(Vector2, newVector2, defendingFigures, attackingFigures, fieldsStatus, kingVector2),
            _ => throw new InvalidOperationException(),
            };
    }
}
