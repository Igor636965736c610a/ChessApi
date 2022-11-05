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
        public Vector2 Vector2 { get; set; }
        public Value Value { get; set; }
        public bool WhiteColor { get; set; }
        public FigureType FigureType { get; set; }

        protected Figure(Value value, bool color, Vector2 vector2)
        {
            Value = value;
            WhiteColor = color;
            Vector2 = vector2;
        }

        public abstract bool ChcekLegalMovement(Board board, Vector2 newVector2, List<Figure> attackingFigures);

        public abstract bool[,] ShowLegalMovement(Board board, List<Figure> attackingFigures);

        //public abstract void SetAttackFields(Figure[,] fieldsStatus, bool[,] newAttackedFields);

        public virtual void SetNewPosition(Vector2 newVector2)
        {
            Vector2 = new Vector2(newVector2.X, newVector2.Y);
        }
    }   
}
