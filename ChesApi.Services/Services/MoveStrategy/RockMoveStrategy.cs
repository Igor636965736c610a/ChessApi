using ChesApi.Infrastructure.Services.AttackedFiels;
using ChesApi.Infrastructure.Services.EnumFiguresDirection;
using ChesApi.Infrastructure.Services.MoveStrategy.HelperMethods;
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
                        GlobalStrategyMethods.UpMovement(oldY, newX, newY,liveGame);
                        if (GlobalStrategyMethods.CheckSetNewPosition(figure, liveGame, newX, newY, oldY, oldX,
                            _setNewAttackFieles, _figureRepository))
                            GlobalStrategyMethods.SetNewPosition(figure, liveGame, newX, newY, _figureRepository);
                        else
                            throw new InvalidOperationException();

                        break;
                    }
                case EnumDirection.Down:
                    {
                        GlobalStrategyMethods.DownMovement(oldY, newX, newY, liveGame);
                        if (GlobalStrategyMethods.CheckSetNewPosition(figure, liveGame, newX, newY, oldY, oldX,
                            _setNewAttackFieles, _figureRepository))
                            GlobalStrategyMethods.SetNewPosition(figure, liveGame, newX, newY, _figureRepository);
                        else
                            throw new InvalidOperationException();

                        break;
                    }
                case EnumDirection.Left:
                    {
                        GlobalStrategyMethods.LeftMovement(oldX, newX, newY, liveGame);
                        if (GlobalStrategyMethods.CheckSetNewPosition(figure, liveGame, newX, newY, oldY, oldX,
                            _setNewAttackFieles, _figureRepository))
                            GlobalStrategyMethods.SetNewPosition(figure, liveGame, newX, newY, _figureRepository);
                        else
                            throw new InvalidOperationException();

                        break;
                    }
                case EnumDirection.Right:
                    {
                        GlobalStrategyMethods.RightMovement(oldX, newX, newY, liveGame);
                        if (GlobalStrategyMethods.CheckSetNewPosition(figure, liveGame, newX, newY, oldY, oldX,
                            _setNewAttackFieles, _figureRepository))
                            GlobalStrategyMethods.SetNewPosition(figure, liveGame, newX, newY, _figureRepository);
                        else
                            throw new InvalidOperationException();

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
            GlobalStrategyMethods.UpSetAttackFieles(fielsStatus, newAttackedFiels, figure.Y, figure.X, king, figure);
            GlobalStrategyMethods.DownSetAttackFieles(fielsStatus, newAttackedFiels, figure.Y, figure.X, king, figure);
            GlobalStrategyMethods.LeftSetAttackFieles(fielsStatus, newAttackedFiels, figure.Y, figure.X, king, figure);
            GlobalStrategyMethods.RightSetAttackFieles(fielsStatus, newAttackedFiels, figure.Y, figure.X, king, figure);
        }
        public bool CheckCheckMate(int x, int y, Figure king, IEnumerable<Figure> defendingFigures, 
            FielsStatus[,] fielsStatus, EnumDirection direction, IFigureTypeMoveStrategySelector figureTypeMoveStrategySelector) 
        {
            switch(direction)
            {
                case EnumDirection.Up:
                    {
                        return GlobalStrategyMethods.UpAttack
                            (x, y, king.Y, king, defendingFigures, fielsStatus, figureTypeMoveStrategySelector);
                    }
                case EnumDirection.Down:
                    {
                        return GlobalStrategyMethods.DownAttack
                            (x, y, king.Y, king, defendingFigures, fielsStatus, figureTypeMoveStrategySelector);
                    }
                case EnumDirection.Left:
                    {
                        return GlobalStrategyMethods.LeftAttack
                            (x, y, king.X, king, defendingFigures, fielsStatus, figureTypeMoveStrategySelector);
                    }
                case EnumDirection.Right:
                    {
                        return GlobalStrategyMethods.RightAttack
                            (x, y, king.X, king, defendingFigures, fielsStatus, figureTypeMoveStrategySelector);
                    }
                default:
                    {
                        throw new InvalidOperationException();
                    }
            }
        }

        public bool CheckLegalMoveDirection(int oldX, int oldY, int newX, int newY)
        {
            if (oldX != newX || oldY != newY)
            {
                return true;
            }
            return false;
        }
    }
}
