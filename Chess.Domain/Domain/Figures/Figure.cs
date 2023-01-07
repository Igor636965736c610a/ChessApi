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
    public abstract class Figure
    {
        public Vector2 Vector2 { get; set; }
        public int Value { get; protected set; }
        public bool WhiteColor { get; set; }
        public  FigureType FigureType { get; set; }
        public char FigureChar { get; set; }

        protected Figure(bool color, Vector2 vector2)
        {
            WhiteColor = color;
            Vector2 = vector2;
        }

        public abstract bool CheckLegalMovement(Board board, Figure?[,] fieldsStatus, Vector2 newVector2, IEnumerable<Figure> enemyFigures, Figure? king);

        public abstract IEnumerable<Vector2> ShowLegalMovement(Board board, IEnumerable<Figure> attackingFigures);

        public virtual void SetNewPosition(Vector2 newVector2, Board board)
        {
            UtilsMethods.SetNewPosition(newVector2, board, this);
        }

        public abstract IEnumerable<Vector2> Dirs { get; }
    }   
}
