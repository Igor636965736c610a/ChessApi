using Chess.Core.Domain.EnumsAndStructs;
using Chess.Core.Domain.Figures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Core.Domain
{
    public class Board
    {
        public Board(List<Figure> figures, Figure[,] fieldsStatus, Vector2 maxDimensions)
        {
            Figures = figures;
            FieldsStatus = fieldsStatus;
            XMax = maxDimensions.X;
            YMax = maxDimensions.Y;
        }
        public List<Figure> Figures { get; set; }
        public Figure?[,] FieldsStatus { get; set; }
        public EnPassant EnPassant { get; set; }
        public int XMin { get; } = 0;
        public int YMin { get; } = 0;
        public int XMax { get; }
        public int YMax { get; }
    }
}
