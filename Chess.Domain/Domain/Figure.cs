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
        public bool IsDeleted { get; set; } = false;
        public FigureType FigureType { get; set; }
        public Value Value { get; set; }
        public FigureColour Colour { get; set; }

        public Figure(int x, int y, FigureType figureType, Value value, FigureColour colour)
        {
            X = x;
            Y = y;
            FigureType = figureType;
            Value = value;
            Colour = colour;
        }
        public Figure(int x, int y, bool isDeleted, FigureType figureType, Value value, FigureColour colour)
        {
            X = x;
            Y = y;
            IsDeleted = isDeleted;
            FigureType = figureType;
            Value = value;
            Colour = colour;
        }
    }
}
