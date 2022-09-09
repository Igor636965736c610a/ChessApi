using ChesApi.Infrastructure.Services.EnumFiguresDirection;
using Chess.Core.Domain.Enums;
using Chess.Core.Domain.EnumsAndStructs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Core.Domain.Figures
{
    public abstract class Figure
    {
        public Guid Id { get; set; }
        public Vector2 Vector2 { get; set; }
        public Value Value { get; set; }
        public FigureColor Color { get; set; }
        public FigureType FigureType { get; set; }
        public bool IsAttacking { get; set; } = false;

        protected Figure(Value value, FigureColor color, Vector2 vector2, Guid id)
        {
            Value = value;
            Color = color;
            Vector2 = vector2;
            Id = id;
        }

        public virtual void SetAttackFields(FieldsStatus[,] fieldsStatus, bool[,] newAttackedFields)
        {
        }
        public virtual EnumDirection SetDirection(Vector2 newVector2)
        {
            throw new InvalidOperationException();
        }
        public virtual bool ChcekLegalMovement(FieldsStatus[,] fieldsStatus, Vector2 newVector2, EnumDirection enumDirection)
        {
            return false;
        }
        public virtual bool CheckLegalMoveDirection(Vector2 newVector2)
        {
            return false;
        }
        public virtual void SetNewPosition(Vector2 newVector2)
        {
        }
        public virtual bool CheckCheckamte(Vector2 newVector2, IEnumerable<Figure> defendingFigures,
            IEnumerable<Figure> attackingFigures, FieldsStatus[,] fieldsStatus, Vector2 kingVector2, EnumDirection direction)
        {
            throw new InvalidOperationException();
        }
    }   
}
