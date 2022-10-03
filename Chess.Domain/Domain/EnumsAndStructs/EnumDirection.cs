using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Chess.Core.Domain.Figures.Knight;

namespace ChesApi.Infrastructure.Services.EnumFiguresDirection
{
    public enum EnumDirection
    {
        Up = 0,
        Down = 1,
        Left = 2,
        Right = 3,
        UpLeft = 4,
        DownLeft = 5,
        UpRight = 6,
        DownRight = 7,
        KnightDownLeft = 8,
        KnightUpLeft = 9,
        KnightDownRight = 10,
        KnightUpRight = 11,
        KnightLeftDown = 12,
        KnightLeftUp = 13,
        KnightRightDown = 14,
        KnightRightUp = 15,
    }
}
