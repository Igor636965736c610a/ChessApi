using ChesApi.Infrastructure.Services.AttackedFiels;
using Chess.Core.Domain;
using Chess.Core.Domain.Enums;
using Chess.Core.Domain.EnumsAndStructs;
using Chess.Core.Repo.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChesApi.Infrastructure.Services.MoveStrategy.HelperMethods
{
    public static class HelperStrategyMethods
    {
        public static void UpSetAttackFieles(FielsStatus[,] fielsStatus, bool[,] newAttackedFiels, int y, int x, Figure? king,
            Figure figure)
        {
            for (int i = y - 1; i >= 0; i--)
            {
                newAttackedFiels[i, x] = true;
                if (fielsStatus[i, x].OccupiedBlackFiels || fielsStatus[i, x].OccupiedWhiteFiels)
                {
                    KingAttacking(king, figure, i, x);
                    break;
                }
            }
        }
        public static void DownSetAttackFieles(FielsStatus[,] fielsStatus, bool[,] newAttackedFiels, int y, int x, Figure? king,
            Figure figure)
        {
            for (int i = y + 1; i < 8; i++)
            {
                newAttackedFiels[i, x] = true;
                if (fielsStatus[i, x].OccupiedBlackFiels || fielsStatus[i, x].OccupiedWhiteFiels)
                {
                    KingAttacking(king, figure, i, x);
                    break;
                }
            }
        }
        public static void LeftSetAttackFieles(FielsStatus[,] fielsStatus, bool[,] newAttackedFiels, int y, int x, Figure? king,
            Figure figure)
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
        public static void RightSetAttackFieles(FielsStatus[,] fielsStatus, bool[,] newAttackedFiels, int y, int x, Figure? king,
            Figure figure)
        {
            for (int i = x + 1; i < 8; i++)
            {
                newAttackedFiels[y, i] = true;
                if (fielsStatus[y, i].OccupiedBlackFiels || fielsStatus[y, i].OccupiedWhiteFiels)
                {
                    KingAttacking(king, figure, y, i);
                    break;
                }
            }
        }
        // UpLeft
        // UpRight
        // ...
        public static void UpMovement(int oldY, int x, int y, Figure figure, LiveGame liveGame)
        {
            for (int i = oldY - 1; i > y; i--)
            {
                if (liveGame.FielsStatus[i, x].OccupiedBlackFiels || liveGame.FielsStatus[i, x].OccupiedWhiteFiels)
                {
                    throw new InvalidOperationException();
                }
            }
        }
        public static void DownMovement(int oldY, int x, int y, Figure figure, LiveGame liveGame)
        {
            for (int i = oldY + 1; i < y; i++)
            {
                if (liveGame.FielsStatus[i, x].OccupiedBlackFiels || liveGame.FielsStatus[i, x].OccupiedWhiteFiels)
                {
                    throw new InvalidOperationException();
                }
            }
        }
        public static void LeftMovement(int oldX, int x, int y, Figure figure, LiveGame liveGame)
        {
            for (int i = oldX - 1; i > x; i--)
            {
                if (liveGame.FielsStatus[i, x].OccupiedBlackFiels || liveGame.FielsStatus[i, x].OccupiedWhiteFiels)
                {
                    throw new InvalidOperationException();
                }
            }
        }
        public static void RightMovement(int oldX, int x, int y, Figure figure, LiveGame liveGame)
        {
            for (int i = oldX + 1; i < x; i++)
            {
                if (liveGame.FielsStatus[i, x].OccupiedBlackFiels || liveGame.FielsStatus[i, x].OccupiedWhiteFiels)
                {
                    throw new InvalidOperationException();
                }
            }
        }
        // UpLeft
        // UpRight
        // ...
        public static void SetNewPosition(Figure figure, LiveGame liveGame, int x, int y, int oldY, int oldX, 
            ISetNewAttackFieles? setNewAttackFieles, IFigureRepository? figureRepository)
        {
            if (setNewAttackFieles is null || figureRepository is null)
            {
                throw new Exception("Tego bledu tu nie powinno byc");
            }
            switch (figure.Colour)
            {
                case FigureColour.white:
                    {
                        if (liveGame.FielsStatus[y, x].OccupiedWhiteFiels)
                        {
                            throw new InvalidOperationException();
                        }
                        liveGame.FielsStatus[oldY, oldX].OccupiedWhiteFiels = false;
                        var newAttackedBlackFiels = setNewAttackFieles.SetNewAttackFieles(liveGame, FigureColour.black, null);
                        var king = figureRepository.GetKing(liveGame, FigureColour.white);
                        if (newAttackedBlackFiels[king.Y, king.X])
                        {
                            liveGame.FielsStatus[oldY, oldX].OccupiedWhiteFiels = true;
                            throw new InvalidOperationException();
                        }
                        if (liveGame.FielsStatus[y, x].OccupiedBlackFiels)
                        {
                            var toDeleteFigure = figureRepository.GetFigure(liveGame, y, x);
                            figureRepository.RemoveFigure(liveGame, toDeleteFigure);
                        }
                        liveGame.FielsStatus[y, x].OccupiedWhiteFiels = true;
                        figure.X = x;
                        figure.Y = y;
                        // for (int i = 0; i < liveGame.FielsStatus.GetLength(0); i++)
                        // {
                        //     for (int z = 0; z < liveGame.FielsStatus.GetLength(1); z++)
                        //     {
                        //         liveGame.FielsStatus[i, z].AttackedBlackFiels = newAttackedBlackFiels[i, z];
                        //     }
                        // }
                        break;
                    }
                case FigureColour.black:
                    {
                        if (liveGame.FielsStatus[y, x].OccupiedBlackFiels)
                        {
                            throw new InvalidOperationException();
                        }
                        liveGame.FielsStatus[oldY, oldX].OccupiedBlackFiels = false;
                        var newAttackedWhiteFiels = setNewAttackFieles.SetNewAttackFieles(liveGame, FigureColour.white, null);
                        var king = figureRepository.GetKing(liveGame, FigureColour.black);
                        if (newAttackedWhiteFiels[king.Y, king.X])
                        {
                            liveGame.FielsStatus[oldY, oldX].OccupiedBlackFiels = true;
                            throw new InvalidOperationException();
                        }
                        if (liveGame.FielsStatus[y, x].OccupiedWhiteFiels)
                        {
                            var toDeleteFigure = figureRepository.GetFigure(liveGame, y, x);
                            figureRepository.RemoveFigure(liveGame, toDeleteFigure);
                        }
                        liveGame.FielsStatus[y, x].OccupiedBlackFiels = true;
                        figure.X = x;
                        figure.Y = y;
                        // for (int i = 0; i < liveGame.FielsStatus.GetLength(0); i++)
                        // {
                        //     for (int z = 0; z < liveGame.FielsStatus.GetLength(1); z++)
                        //     {
                        //         liveGame.FielsStatus[i, z].AttackedWhiteFiels = newAttackedWhiteFiels[i, z];
                        //     }
                        // }
                        break;
                    }
            }
        }
        private static void KingAttacking(Figure? king, Figure figure, int y, int x)
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
