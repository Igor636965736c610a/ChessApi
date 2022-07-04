using Chess.Core.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChesApi.Services.PrivateDto
{
    public class PrivateFigureDto
    {
        public Guid Id { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public bool IsDeleted { get; set; } = false;
        public FigureType FigureType { get; set; }
        public Value Value { get; set; }
        public FigureColour Colour { get; set; }
    }
}
