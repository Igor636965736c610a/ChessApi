using Chess.Core.Domain.Enums;
using Chess.Core.Domain.EnumsAndStructs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Core.Domain.Figures
{
    public class Knight : Figure
    {
        public Knight(Value value, FigureColor color, Vector2 vector2, Guid id) : base(value, color, vector2, id)
        {
        }
    }
}
