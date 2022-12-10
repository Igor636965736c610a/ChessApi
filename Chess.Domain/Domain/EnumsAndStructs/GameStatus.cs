using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Core.Domain.EnumsAndStructs
{
    public enum GameStatus
    {
        WhiteCheckMate = 0,
        BlackCheckMate = 1,
        IsGaming = 2,
        WhiteCheck = 3,
        BlackCheck = 4,
        Pat = 5,
    }
}
