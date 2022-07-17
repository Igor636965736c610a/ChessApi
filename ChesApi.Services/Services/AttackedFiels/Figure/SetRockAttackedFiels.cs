using Chess.Core.Domain.DefaultConst;
using Chess.Core.Domain.EnumsAndStructs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChesApi.Services.Services.AttackedFiels.Figure
{
    public static class SetRockAttackedFiels
    {
        public static void RockAttakedFiels(FielsStatus[,] fielsStatus, bool[,] newAttackedFiels, int y, int x)
        {
            Up(fielsStatus, newAttackedFiels, y, x);
            Down(fielsStatus, newAttackedFiels, y, x);
            Left(fielsStatus, newAttackedFiels, y, x);
            Right(fielsStatus, newAttackedFiels, y, x);
        }
        private static void Up(FielsStatus[,] fielsStatus, bool[,] newAttackedFiels, int y, int x)
        {
            for(int i = y - 1; i >= 0; i--)
            {
                newAttackedFiels[i, x] = true;
                if(fielsStatus[i, x].OccupiedBlackFiels || fielsStatus[i, x].OccupiedWhiteFiels)
                {
                    break;
                }
            }
        }
        private static void Down(FielsStatus[,] fielsStatus, bool[,] newAttackedFiels, int y, int x)
        {
            for (int i = y + 1; i <= 7; i++)
            {
                newAttackedFiels[i, x] = true;
                if (fielsStatus[i, x].OccupiedBlackFiels || fielsStatus[i, x].OccupiedWhiteFiels)
                {
                    break;
                }
            }
        }
        private static void Left(FielsStatus[,] fielsStatus, bool[,] newAttackedFiels, int y, int x)
        {
            for (int i = x - 1; i >= 0; i--)
            {
                newAttackedFiels[y, i] = true;
                if (fielsStatus[y, i].OccupiedBlackFiels || fielsStatus[y, i].OccupiedWhiteFiels)
                {
                    break;
                }
            }
        }
        private static void Right(FielsStatus[,] fielsStatus, bool[,] newAttackedFiels, int y, int x)
        {
            for (int i = x + 1; i <= 7; i++)
            {
                newAttackedFiels[y, i] = true;
                if (fielsStatus[y, i].OccupiedBlackFiels || fielsStatus[y, i].OccupiedWhiteFiels)
                {
                    break;
                }
            }
        }
    }
}
