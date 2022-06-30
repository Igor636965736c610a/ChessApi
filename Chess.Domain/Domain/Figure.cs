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
        public int[,] Coordinates { get; set; }
        public bool IsDeleted { get; set; } = false;
        public FigureType FigureType { get; set; }
        public Value Value { get; set; }
        public FigureColour Colour { get; set; }

        public Figure(int[,] coordinates, FigureType figureType, Value value, FigureColour colour)
        {
            Coordinates = coordinates;
            FigureType = figureType;
            Value = value;
            Colour = colour;
        }
        public Figure(int[,] coordinates, bool isDeleted, FigureType figureType, Value value, FigureColour colour)
        {
            Coordinates = coordinates;
            IsDeleted = isDeleted;
            FigureType = figureType;
            Value = value;
            Colour = colour;
        }
    }
}
