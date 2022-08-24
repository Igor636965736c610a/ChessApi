using ChesApi.Infrastructure.Services.AttackedFiels;
using ChesApi.Infrastructure.Services.EnumFiguresDirection;
using ChesApi.Infrastructure.Services.MoveStrategy.HelperMethods;
using ChesApi.Infrastructure.Services.MoveStrategy.MoveDirectionStrategy.SelectLogic;
using Chess.Core.Domain;
using Chess.Core.Domain.EnumsAndStructs;
using Chess.Core.Repo.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChesApi.Infrastructure.Services.MoveStrategy
{
    public class QueenMoveStrategy : IFigureTypeMoveStrategy
    {
        private readonly IFigureRepository? _figureRepository;
        private readonly ISetNewAttackFields? _setNewAttackFields;
        public QueenMoveStrategy(IFigureRepository? figureRepository, ISetNewAttackFields? setNewAttackFields)
        {
            _figureRepository = figureRepository;
            _setNewAttackFields = setNewAttackFields;
        }
        public bool ChcekLegalMovement(Figure figure, FieldsStatus[,] fieldsStatus, int oldX, int oldY, int newX, int newY, EnumDirection enumDirection)
        {
            switch (enumDirection)
            {
                case EnumDirection.Up:
                        return GlobalStrategyMethods.UpMovement(oldY, newX, newY, fieldsStatus);
                case EnumDirection.Down:
                        return GlobalStrategyMethods.DownMovement(oldY, newX, newY, fieldsStatus);
                case EnumDirection.Left:
                        return GlobalStrategyMethods.LeftMovement(oldX, newX, newY, fieldsStatus);
                case EnumDirection.Right:
                        return GlobalStrategyMethods.RightMovement(oldX, newX, newY, fieldsStatus);
                case EnumDirection.UpLeft:
                        return GlobalStrategyMethods.UpLeftMovement(oldY, oldX, newX, newY, fieldsStatus);
                case EnumDirection.UpRight:
                        return GlobalStrategyMethods.UpRightMovement(oldY, oldX, newX, newY, fieldsStatus);
                case EnumDirection.DownLeft:
                        return GlobalStrategyMethods.DownLeftMovement(oldY, oldX, newX, newY, fieldsStatus);
                case EnumDirection.DownRight:
                        return GlobalStrategyMethods.DownRightMovement(oldY, oldX, newX, newY, fieldsStatus);
                default:
                        throw new InvalidOperationException();
            }
        }

        public bool CheckCheckMate(int x, int y, Figure king, IEnumerable<Figure> defendingFigures, LiveGame liveGame, EnumDirection direction, IFigureTypeMoveStrategySelector figureTypeMoveStrategySelector)
        {
            if (_figureRepository is null || _setNewAttackFields is null)
                throw new InvalidOperationException();

            switch (direction)
            {
                case EnumDirection.Up:
                    return GlobalStrategyMethods.UpAttack
                        (x, y, king.Y, defendingFigures, liveGame, figureTypeMoveStrategySelector, _figureRepository,
                        _setNewAttackFields);
                case EnumDirection.Down:
                    return GlobalStrategyMethods.DownAttack
                        (x, y, king.Y, defendingFigures, liveGame, figureTypeMoveStrategySelector, _figureRepository,
                        _setNewAttackFields);
                case EnumDirection.Left:
                    return GlobalStrategyMethods.LeftAttack
                        (x, y, king.X, defendingFigures, liveGame, figureTypeMoveStrategySelector, _figureRepository,
                        _setNewAttackFields);
                case EnumDirection.Right:
                    return GlobalStrategyMethods.RightAttack
                        (x, y, king.X, defendingFigures, liveGame, figureTypeMoveStrategySelector, _figureRepository,
                        _setNewAttackFields);
                case EnumDirection.UpLeft:
                    return GlobalStrategyMethods.UpLeftAttack
                        (x, y, king.X, king.Y, defendingFigures, liveGame, figureTypeMoveStrategySelector, _figureRepository,
                        _setNewAttackFields);
                case EnumDirection.UpRight:
                    return GlobalStrategyMethods.UpRightAttack
                        (x, y, king.X, king.Y, defendingFigures, liveGame, figureTypeMoveStrategySelector, _figureRepository,
                        _setNewAttackFields);
                case EnumDirection.DownLeft:
                    return GlobalStrategyMethods.DownLeftAttack
                        (x, y, king.X, king.Y, defendingFigures, liveGame, figureTypeMoveStrategySelector, _figureRepository,
                        _setNewAttackFields);
                case EnumDirection.DownRight:
                    return GlobalStrategyMethods.DownRightAttack
                        (x, y, king.X, king.Y, defendingFigures, liveGame, figureTypeMoveStrategySelector, _figureRepository,
                        _setNewAttackFields);
                default:
                    throw new InvalidOperationException();
            }
        }

        public bool CheckLegalMoveDirection(int oldX, int oldY, int newX, int newY)
        {
            if (oldX != newX && oldY == newY || oldY != newY && oldX == newX || Math.Abs(oldX - newX) == Math.Abs(oldY - newY))
                return true;
            return false;
        }

        public void Move(Figure figure, LiveGame liveGame, int oldX, int oldY, int newX, int newY, EnumDirection enumDirection)
        {
            if (_figureRepository is null || _setNewAttackFields is null)
                throw new InvalidOperationException();

            switch (enumDirection)
            {
                case EnumDirection.Up:
                    {
                        if (!GlobalStrategyMethods.UpMovement(oldY, newX, newY, liveGame.FieldsStatus))
                            throw new InvalidOperationException();
                        if (GlobalStrategyMethods.CheckSetNewPosition(figure, liveGame, newX, newY, oldY, oldX,
                            _setNewAttackFields, _figureRepository))
                            GlobalStrategyMethods.SetNewPosition(figure, liveGame, newX, newY, _figureRepository);
                        else
                            throw new InvalidOperationException();

                        break;
                    }
                case EnumDirection.Down:
                    {
                        if (!GlobalStrategyMethods.DownMovement(oldY, newX, newY, liveGame.FieldsStatus))
                            throw new InvalidOperationException();
                        if (GlobalStrategyMethods.CheckSetNewPosition(figure, liveGame, newX, newY, oldY, oldX,
                            _setNewAttackFields, _figureRepository))
                            GlobalStrategyMethods.SetNewPosition(figure, liveGame, newX, newY, _figureRepository);
                        else
                            throw new InvalidOperationException();

                        break;
                    }
                case EnumDirection.Left:
                    {
                        if (!GlobalStrategyMethods.LeftMovement(oldX, newX, newY, liveGame.FieldsStatus))
                            throw new InvalidOperationException();
                        if (GlobalStrategyMethods.CheckSetNewPosition(figure, liveGame, newX, newY, oldY, oldX,
                            _setNewAttackFields, _figureRepository))
                            GlobalStrategyMethods.SetNewPosition(figure, liveGame, newX, newY, _figureRepository);
                        else
                            throw new InvalidOperationException();

                        break;
                    }
                case EnumDirection.Right:
                    {
                        if (!GlobalStrategyMethods.RightMovement(oldX, newX, newY, liveGame.FieldsStatus))
                            throw new InvalidOperationException();
                        if (GlobalStrategyMethods.CheckSetNewPosition(figure, liveGame, newX, newY, oldY, oldX,
                            _setNewAttackFields, _figureRepository))
                            GlobalStrategyMethods.SetNewPosition(figure, liveGame, newX, newY, _figureRepository);
                        else
                            throw new InvalidOperationException();

                        break;
                    }
                case EnumDirection.UpLeft:
                    {
                        if (!GlobalStrategyMethods.UpLeftMovement(oldX, oldY, newX, newY, liveGame.FieldsStatus))
                            throw new InvalidOperationException();
                        if (GlobalStrategyMethods.CheckSetNewPosition(figure, liveGame, newX, newY, oldY, oldX,
                            _setNewAttackFields, _figureRepository))
                            GlobalStrategyMethods.SetNewPosition(figure, liveGame, newX, newY, _figureRepository);
                        else
                            throw new InvalidOperationException();

                        break;
                    }
                case EnumDirection.UpRight:
                    {
                        if (!GlobalStrategyMethods.UpRightMovement(oldX, oldY, newX, newY, liveGame.FieldsStatus))
                            throw new InvalidOperationException();
                        if (GlobalStrategyMethods.CheckSetNewPosition(figure, liveGame, newX, newY, oldY, oldX,
                            _setNewAttackFields, _figureRepository))
                            GlobalStrategyMethods.SetNewPosition(figure, liveGame, newX, newY, _figureRepository);
                        else
                            throw new InvalidOperationException();

                        break;
                    }
                case EnumDirection.DownLeft:
                    {
                        if (!GlobalStrategyMethods.DownLeftMovement(oldX, oldY, newX, newY, liveGame.FieldsStatus))
                            throw new InvalidOperationException();
                        if (GlobalStrategyMethods.CheckSetNewPosition(figure, liveGame, newX, newY, oldY, oldX,
                            _setNewAttackFields, _figureRepository))
                            GlobalStrategyMethods.SetNewPosition(figure, liveGame, newX, newY, _figureRepository);
                        else
                            throw new InvalidOperationException();

                        break;
                    }
                case EnumDirection.DownRight:
                    {
                        if (!GlobalStrategyMethods.DownRightMovement(oldX, oldY, newX, newY, liveGame.FieldsStatus))
                            throw new InvalidOperationException();
                        if (GlobalStrategyMethods.CheckSetNewPosition(figure, liveGame, newX, newY, oldY, oldX,
                            _setNewAttackFields, _figureRepository))
                            GlobalStrategyMethods.SetNewPosition(figure, liveGame, newX, newY, _figureRepository);
                        else
                            throw new InvalidOperationException();

                        break;
                    }
            }
        }

        public void SetAttackFieles(FieldsStatus[,] fieldsStatus, bool[,] newAttackedFields, Figure figure, Figure? king)
        {
            GlobalStrategyMethods.UpSetAttackFields(fieldsStatus, newAttackedFields, figure.Y, figure.X, king, figure);
            GlobalStrategyMethods.DownSetAttackFields(fieldsStatus, newAttackedFields, figure.Y, figure.X, king, figure);
            GlobalStrategyMethods.LeftSetAttackFields(fieldsStatus, newAttackedFields, figure.Y, figure.X, king, figure);
            GlobalStrategyMethods.RightSetAttackFields(fieldsStatus, newAttackedFields, figure.Y, figure.X, king, figure);
            GlobalStrategyMethods.UpRightSetAttackFields(fieldsStatus, newAttackedFields, figure.Y, figure.X, king, figure);
            GlobalStrategyMethods.UpLeftSetAttackFields(fieldsStatus, newAttackedFields, figure.Y, figure.X, king, figure);
            GlobalStrategyMethods.DownRightSetAttackFields(fieldsStatus, newAttackedFields, figure.Y, figure.X, king, figure);
            GlobalStrategyMethods.DownLeftSetAttackFields(fieldsStatus, newAttackedFields, figure.Y, figure.X, king, figure);
        }

        public EnumDirection SetDirection(int oldX, int oldY, int newX, int newY)
        {
            if (oldY > newY && oldX == newX)
                return EnumDirection.Down;
            if (oldY < newY && oldX == newX)
                return EnumDirection.Up;
            if (oldX < newX && oldY == newY)
                return EnumDirection.Right;
            if (oldX > newX && oldY == newY)
                return EnumDirection.Left;
            if (oldX > newX && oldY < newY)
                return EnumDirection.UpLeft;
            if (oldX < newX && oldY < newY)
                return EnumDirection.UpRight;
            if (oldX > newX && oldY > newY)
                return EnumDirection.DownLeft;
            if (oldX < newX && oldY > newY)
                return EnumDirection.DownRight;
            throw new InvalidOperationException();
        }
    }
}
