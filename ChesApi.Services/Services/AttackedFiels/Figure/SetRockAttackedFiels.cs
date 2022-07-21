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
        public static void RockAttakedFiels(FielsStatus[,] fielsStatus, bool[,] newAttackedFiels, 
            Chess.Core.Domain.Figure figure, Chess.Core.Domain.Figure king)
        {
            int y = figure.Y;
            int x = figure.X;
            Up(fielsStatus, newAttackedFiels, y, x, king, figure);
            Down(fielsStatus, newAttackedFiels, y, x, king, figure);
            Left(fielsStatus, newAttackedFiels, y, x, king, figure);
            Right(fielsStatus, newAttackedFiels, y, x, king, figure);
        }
        private static void Up(FielsStatus[,] fielsStatus, bool[,] newAttackedFiels, int y, int x, Chess.Core.Domain.Figure king
            ,Chess.Core.Domain.Figure figure)
        {
            for(int i = y - 1; i >= 0; i--)
            {
                newAttackedFiels[i, x] = true;
                if(fielsStatus[i, x].OccupiedBlackFiels || fielsStatus[i, x].OccupiedWhiteFiels)
                {
                    KingAttacking(king, figure, i, x);
                    break;
                }
            }  
        }
        private static void Down(FielsStatus[,] fielsStatus, bool[,] newAttackedFiels, int y, int x, Chess.Core.Domain.Figure king
            ,Chess.Core.Domain.Figure figure)
        {
            for (int i = y + 1; i <= 7; i++)
            {
                newAttackedFiels[i, x] = true;
                if (fielsStatus[i, x].OccupiedBlackFiels || fielsStatus[i, x].OccupiedWhiteFiels)
                {
                    KingAttacking(king, figure, i, x);
                    break;
                }
            }
        }
        private static void Left(FielsStatus[,] fielsStatus, bool[,] newAttackedFiels, int y, int x, Chess.Core.Domain.Figure king
            ,Chess.Core.Domain.Figure figure)
        {
            for (int i = x - 1; i >= 0; i--)
            {
                newAttackedFiels[y, i] = true;
                if (fielsStatus[y, i].OccupiedBlackFiels || fielsStatus[y, i].OccupiedWhiteFiels)
                {
                    KingAttacking(king, figure, y, i);
                    break;
                }
            }
        }
        private static void Right(FielsStatus[,] fielsStatus, bool[,] newAttackedFiels, int y, int x, Chess.Core.Domain.Figure king
            ,Chess.Core.Domain.Figure figure)
        {
            for (int i = x + 1; i <= 7; i++)
            {
                newAttackedFiels[y, i] = true;
                if (fielsStatus[y, i].OccupiedBlackFiels || fielsStatus[y, i].OccupiedWhiteFiels)
                {
                    KingAttacking(king, figure, y, i);
                    break;
                }
            }
        }
        private static void KingAttacking(Chess.Core.Domain.Figure king, Chess.Core.Domain.Figure figure, int y, int x)
        {
            if (king is not null)
            {
                if (king.X == x && king.Y == y && figure.Colour != king.Colour)
                {
                    figure.IsAttacking = true;
                }
            }
        }
    }
}
