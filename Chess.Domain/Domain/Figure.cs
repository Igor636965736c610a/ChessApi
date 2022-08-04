using Chess.Core.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Core.Domain
{
    public class Figure
    {
        public Guid Id { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public bool IsAttacking { get; set; } = false;
        public FigureType FigureType { get; set; }
        public Value Value { get; set; }
        public FigureColor Color { get; set; }

        public Figure(int x, int y, FigureType figureType, Value value, FigureColor color)
        {
            X = x;
            Y = y;
            FigureType = figureType;
            Value = value;
            Color = color;
        }
    }
}
