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
                        HelperStrategyMethods.UpMovement(oldY, newX, newY, figure, liveGame);
                        HelperStrategyMethods.SetNewPosition(figure, liveGame, newX, newY, oldY, newX, 
                            _setNewAttackFieles, _figureRepository);
                        break;
                    }
                case EnumDirection.Down:
                    {
                        HelperStrategyMethods.DownMovement(oldY, newX, newY, figure, liveGame);
                        HelperStrategyMethods.SetNewPosition(figure, liveGame, newX, newY, oldY, newX, 
                            _setNewAttackFieles, _figureRepository);
                        break;
                    }
                case EnumDirection.Left:
                    {
                        HelperStrategyMethods.LeftMovement(oldX, newX, newY, figure, liveGame);
                        HelperStrategyMethods.SetNewPosition(figure, liveGame, newX, newY, newY, oldX, 
                            _setNewAttackFieles, _figureRepository);
                        break;
                    }
                case EnumDirection.Right:
                    {
                        HelperStrategyMethods.RightMovement(oldX, newX, newY, figure, liveGame);
                        HelperStrategyMethods.SetNewPosition(figure, liveGame, newX, newY, newY, oldX, 
                            _setNewAttackFieles, _figureRepository);
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
            HelperStrategyMethods.UpSetAttackFieles(fielsStatus, newAttackedFiels, figure.Y, figure.X, king, figure);
            HelperStrategyMethods.DownSetAttackFieles(fielsStatus, newAttackedFiels, figure.Y, figure.X, king, figure);
            HelperStrategyMethods.LeftSetAttackFieles(fielsStatus, newAttackedFiels, figure.Y, figure.X, king, figure);
            HelperStrategyMethods.RightSetAttackFieles(fielsStatus, newAttackedFiels, figure.Y, figure.X, king, figure);
        }
    }
}
