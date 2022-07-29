using ChesApi.Infrastructure.Services.AttackedFiels;
using ChesApi.Infrastructure.Services.EnumFiguresDirection;
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

namespace ChesApi.Infrastructure.Services.MoveStrategy
{
    public class RockMoveStrategy : IFigureTypeMoveStrategy
    {
        private readonly IFigureRepository? _figureRepository;
        private readonly ISetNewAttackFieles? _setNewAttackFieles;
        public RockMoveStrategy(IFigureRepository? figureRepository, ISetNewAttackFieles? setNewAttackFieles)
        {
            _figureRepository = figureRepository;
            _setNewAttackFieles = setNewAttackFieles;
        }
        public void Move(Figure figure, LiveGame liveGame, int oldX, int oldY, int newX, int newY, EnumDirection enumDirection)
        {
            switch (enumDirection)
            {
                case EnumDirection.Up:
                    {
                        RockUpMovement(oldY, newX, newY, figure, liveGame);
                        break;
                    }
                case EnumDirection.Down:
                    {
                        RockDownMovement(oldY, newX, newY, figure, liveGame);
                        break;
                    }
                case EnumDirection.Left:
                    {
                        RockLeftMovement(oldX, newX, newY, figure, liveGame);
                        break;
                    }
                case EnumDirection.Right:
                    {
                        RockRightMovement(oldX, newX, newY, figure, liveGame);
                        break;
                    }
            }
        }
        public EnumDirection SetDirection(int oldX, int oldY, int newX, int newY)
        {
            if (oldX < newX && oldY == newY)
            {
                return EnumDirection.Down;
            }
            if (oldX > newX && oldY == newY)
            {
                return EnumDirection.Up;
            }
            if (oldY < newY && oldX == newX)
            {
                return EnumDirection.Right;
            }
            if (oldY > newY && oldX == newX)
            {
                return EnumDirection.Left;
            }
            else
            {
                throw new InvalidOperationException();
            }
        }
        public void SetAttackFieles(FielsStatus[,] fielsStatus, bool[,] newAttackedFiels, Figure figure, Figure? king)
        {
            int y = figure.Y;
            int x = figure.X;
            Up(fielsStatus, newAttackedFiels, y, x, king, figure);
            Down(fielsStatus, newAttackedFiels, y, x, king, figure);
            Left(fielsStatus, newAttackedFiels, y, x, king, figure);
            Right(fielsStatus, newAttackedFiels, y, x, king, figure);
        }
        private void RockUpMovement(int oldY, int x, int y, Figure figure, LiveGame liveGame)
        {
            for (int i = oldY - 1; i > y; i--)
            {
                if (liveGame.FielsStatus[i, x].OccupiedBlackFiels || liveGame.FielsStatus[i, x].OccupiedWhiteFiels)
                {
                    throw new InvalidOperationException();
                }
            }
            SetNewPositoin(figure, liveGame, x, y, oldY, x);
        }
        private void RockDownMovement(int oldY, int x, int y, Figure figure, LiveGame liveGame)
        {
            for (int i = oldY + 1; i < y; i++)
            {
                if (liveGame.FielsStatus[i, x].OccupiedBlackFiels || liveGame.FielsStatus[i, x].OccupiedWhiteFiels)
                {
                    throw new InvalidOperationException();
                }
            }
            SetNewPositoin(figure, liveGame, x, y, oldY, x);
        }
        private void RockRightMovement(int oldX, int x, int y, Figure figure, LiveGame liveGame)
        {
            for (int i = oldX + 1; i < x; i++)
            {
                if (liveGame.FielsStatus[i, x].OccupiedBlackFiels || liveGame.FielsStatus[i, x].OccupiedWhiteFiels)
                {
                    throw new InvalidOperationException();
                }
            }
            SetNewPositoin(figure, liveGame, x, y, y, oldX);
        }
        private void RockLeftMovement(int oldX, int x, int y, Figure figure, LiveGame liveGame)
        {
            for (int i = oldX - 1; i > x; i--)
            {
                if (liveGame.FielsStatus[i, x].OccupiedBlackFiels || liveGame.FielsStatus[i, x].OccupiedWhiteFiels)
                {
                    throw new InvalidOperationException();
                }
            }
            SetNewPositoin(figure, liveGame, x, y, y, oldX);
        }
        private void SetNewPositoin(Figure figure, LiveGame liveGame, int x, int y, int oldY, int oldX)
        {
            switch (figure.Colour)
            {
                case FigureColour.white:
                    {
                        if (liveGame.FielsStatus[y, x].OccupiedWhiteFiels)
                        {
                            throw new InvalidOperationException();
                        }
                        liveGame.FielsStatus[oldY, oldX].OccupiedWhiteFiels = false;
                        var newAttackedBlackFiels = _setNewAttackFieles.SetNewAttackFieles(liveGame, FigureColour.black, null);
                        var king = _figureRepository.GetKing(liveGame, FigureColour.white);
                        if (newAttackedBlackFiels[king.Y, king.X])
                        {
                            liveGame.FielsStatus[oldY, oldX].OccupiedWhiteFiels = true;
                            throw new InvalidOperationException();
                        }
                        if (liveGame.FielsStatus[y, x].OccupiedBlackFiels)
                        {
                            var toDeleteFigure = _figureRepository.GetFigure(liveGame, y, x);
                            _figureRepository.RemoveFigure(liveGame, toDeleteFigure);
                        }
                        figure.X = x;
                        liveGame.FielsStatus[y, x].OccupiedWhiteFiels = true;
                        for (int i = 0; i < liveGame.FielsStatus.GetLength(0); i++)
                        {
                            for (int z = 0; z < liveGame.FielsStatus.GetLength(1); z++)
                            {
                                liveGame.FielsStatus[i, z].AttackedBlackFiels = newAttackedBlackFiels[i, z];
                            }
                        }
                        break;
                    }
                case FigureColour.black:
                    {
                        if (liveGame.FielsStatus[y, x].OccupiedBlackFiels)
                        {
                            throw new InvalidOperationException();
                        }
                        liveGame.FielsStatus[oldY, oldX].OccupiedBlackFiels = false;
                        var newAttackedWhiteFiels = _setNewAttackFieles.SetNewAttackFieles(liveGame, FigureColour.black, null);
                        var king = _figureRepository.GetKing(liveGame, FigureColour.black);
                        if (newAttackedWhiteFiels[king.Y, king.X])
                        {
                            liveGame.FielsStatus[oldY, oldX].OccupiedBlackFiels = true;
                            throw new InvalidOperationException();
                        }
                        if (liveGame.FielsStatus[y, x].OccupiedWhiteFiels)
                        {
                            var toDeleteFigure = _figureRepository.GetFigure(liveGame, y, x);
                            _figureRepository.RemoveFigure(liveGame, toDeleteFigure);
                        }
                        figure.X = x;
                        liveGame.FielsStatus[y, x].OccupiedBlackFiels = true;
                        for (int i = 0; i < liveGame.FielsStatus.GetLength(0); i++)
                        {
                            for (int z = 0; z < liveGame.FielsStatus.GetLength(1); z++)
                            {
                                liveGame.FielsStatus[i, z].AttackedWhiteFiels = newAttackedWhiteFiels[i, z];
                            }
                        }
                        break;
                    }
            }
        }
        private static void Up(FielsStatus[,] fielsStatus, bool[,] newAttackedFiels, int y, int x, Figure? king,
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
        private static void Down(FielsStatus[,] fielsStatus, bool[,] newAttackedFiels, int y, int x, Figure? king,
            Figure figure)
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
        private static void Left(FielsStatus[,] fielsStatus, bool[,] newAttackedFiels, int y, int x, Figure? king,
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
        private static void Right(FielsStatus[,] fielsStatus, bool[,] newAttackedFiels, int y, int x, Figure? king,
            Figure figure)
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
