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
        public bool WhiteColor { get; set; }
        public FigureType FigureType { get; set; }
        public bool IsAttacking { get; set; } = false;

        protected Figure(Value value, bool color, Vector2 vector2, Guid id)
        {
            Value = value;
            WhiteColor = color;
            Vector2 = vector2;
            Id = id;
        }

        public abstract void SetAttackFields(FieldsStatus[,] fieldsStatus, bool[,] newAttackedFields);

        public abstract bool ChcekLegalMovement(FieldsStatus[,] fieldsStatus, Vector2 newVector2);

        public virtual void SetNewPosition(Vector2 newVector2)
        {
            Vector2 = new Vector2(newVector2.X, newVector2.Y);
        }
    }   
}
