using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Core.Domain.EnumsAndStructs
{
    public enum GameStatus
    {
        WhiteCheck = 0,
        BlackCheck = 1,
        WhiteMat = 2,
        BlackMat = 3,
        IsGaming = 4
    }
}
