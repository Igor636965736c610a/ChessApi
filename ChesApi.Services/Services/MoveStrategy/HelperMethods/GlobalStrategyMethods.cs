using ChesApi.Infrastructure.Services.AttackedFiels;
using ChesApi.Infrastructure.Services.MoveStrategy.MoveDirectionStrategy.SelectLogic;
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
    internal static class GlobalStrategyMethods
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
            switch (figure.Color)
            {
                case FigureColor.White:
                    {
                        if (liveGame.FielsStatus[y, x].OccupiedWhiteFiels)
                        {
                            throw new InvalidOperationException();
                        }
                        liveGame.FielsStatus[oldY, oldX].OccupiedWhiteFiels = false;
                        var newAttackedBlackFiels = setNewAttackFieles.SetNewAttackFieles(liveGame, FigureColor.Black, null);
                        var king = figureRepository.GetKing(liveGame, FigureColor.White);
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
                        break;
                    }
                case FigureColor.Black:
                    {
                        if (liveGame.FielsStatus[y, x].OccupiedBlackFiels)
                        {
                            throw new InvalidOperationException();
                        }
                        liveGame.FielsStatus[oldY, oldX].OccupiedBlackFiels = false;
                        var newAttackedWhiteFiels = setNewAttackFieles.SetNewAttackFieles(liveGame, FigureColor.White, null);
                        var king = figureRepository.GetKing(liveGame, FigureColor.Black);
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
                        break;
                    }
            }
        }
        public static bool UpAttack(int x, int y, int kingY, IEnumerable<Figure> defendingFigures, FielsStatus[,] fielsStatus,
            IFigureTypeMoveStrategySelector figureTypeMoveStrategySelector)
        {
            for (int i = y; i > kingY; i--)
            {
                bool canCover = CheckCover(x, i, defendingFigures, fielsStatus, figureTypeMoveStrategySelector);
                if (canCover)
                {
                    return true;
                }
            }
            return false;
        }
        public static bool DownAttack(int x, int y, int kingY, IEnumerable<Figure> defendingFigures, FielsStatus[,] fielsStatus,
            IFigureTypeMoveStrategySelector figureTypeMoveStrategySelector)
        {
            for (int i = y; i < kingY; i++)
            {
                bool canCover = CheckCover(x, i, defendingFigures, fielsStatus, figureTypeMoveStrategySelector);
                if (canCover)
                {
                    return true;
                }
            }
            return false;
        }
        public static bool LeftAttack(int x, int y, int kingX, IEnumerable<Figure> defendingFigures, FielsStatus[,] fielsStatus,
            IFigureTypeMoveStrategySelector figureTypeMoveStrategySelector)
        {
            for (int i = x; i > kingX; i--)
            {
                bool canCover = CheckCover(i, y, defendingFigures, fielsStatus, figureTypeMoveStrategySelector);
                if (canCover)
                {
                    return true;
                }
            }
            return false;
        }
        public static bool RightAttack(int x, int y, int kingX, IEnumerable<Figure> defendingFigures, FielsStatus[,] fielsStatus,
            IFigureTypeMoveStrategySelector figureTypeMoveStrategySelector)
        {
            for (int i = x; i < kingX; i++)
            {
                bool canCover = CheckCover(i, y, defendingFigures, fielsStatus, figureTypeMoveStrategySelector);
                if (canCover)
                {
                    return true;
                }
            }
            return false;
        }
        // UpDefend
        // DownDefend ...
        private static bool CheckCover(int x, int y, IEnumerable<Figure> defendingFigures, FielsStatus[,] fielsStatus,
            IFigureTypeMoveStrategySelector figureTypeMoveStrategySelector)
        {
            foreach(var f in defendingFigures)
            {
                var figureMoveStrategy = figureTypeMoveStrategySelector.SelectMoveStrategy(f, null, null);
                var legalMove = figureMoveStrategy.CheckLegalMoveDirection(f.X, f.Y, x, y);
                if(legalMove)
                {
                    var direction = figureMoveStrategy.SetDirection(f.X, f.Y, x, y);
                    //bool test = coverMove(direction)
                    //if test jest git return true
                }
            }
            //return false
        }

        private static void KingAttacking(Figure? king, Figure figure, int y, int x)
        {
            if (king is not null)
            {
                if (king.X == x && king.Y == y && figure.Color != king.Color)
                {
                    figure.IsAttacking = true;
                }
            }
        }
    }
}
