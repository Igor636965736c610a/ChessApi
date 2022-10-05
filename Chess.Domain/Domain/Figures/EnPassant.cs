using Chess.Core.Domain.EnumsAndStructs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Core.Domain.Figures
{
    public struct EnPassant
    {
        public EnPassant(bool canEnPassant, Vector2 vector2)
        {
            CanEnPassant = canEnPassant;
            Vector2 = vector2;
        }
        public bool CanEnPassant { get; set; }
        public Vector2 Vector2 { get; set; }
    }
}
