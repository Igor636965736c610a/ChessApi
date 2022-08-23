﻿using ChesApi.Infrastructure.Services.AttackedFiels;
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

namespace ChesApi.Infrastructure.Services.MoveStrategy.HelperMethods
{
    internal static class GlobalStrategyMethods
    {
        public static void UpSetAttackFieles(FieldsStatus[,] fieldsStatus, bool[,] newAttackedFields, int y, int x, Figure? king,
            Figure figure)
        {
            for (int i = y - 1; i >= 0; i--)
            {
                newAttackedFields[i, x] = true;
                if (fieldsStatus[i, x].OccupiedBlackFields || fieldsStatus[i, x].OccupiedWhiteFields)
                {
                    KingAttacking(king, figure, i, x);
                    break;
                }
            }
        }
        public static void DownSetAttackFieles(FieldsStatus[,] fieldsStatus, bool[,] newAttackedFields, int y, int x, Figure? king,
            Figure figure)
        {
            for (int i = y + 1; i < 8; i++)
            {
                newAttackedFields[i, x] = true;
                if (fieldsStatus[i, x].OccupiedBlackFields || fieldsStatus[i, x].OccupiedWhiteFields)
                {
                    KingAttacking(king, figure, i, x);
                    break;
                }
            }
        }
        public static void LeftSetAttackFieles(FieldsStatus[,] fieldsStatus, bool[,] newAttackedFields, int y, int x, Figure? king,
            Figure figure)
        {
            for (int i = x - 1; i >= 0; i--)
            {
                newAttackedFields[y, i] = true;
                if (fieldsStatus[y, i].OccupiedBlackFields || fieldsStatus[y, i].OccupiedWhiteFields)
                {
                    KingAttacking(king, figure, y, i);
                    break;
                }
            }
        }
        public static void RightSetAttackFieles(FieldsStatus[,] fieldsStatus, bool[,] newAttackedFields, int y, int x, Figure? king,
            Figure figure)
        {
            for (int i = x + 1; i < 8; i++)
            {
                newAttackedFields[y, i] = true;
                if (fieldsStatus[y, i].OccupiedBlackFields || fieldsStatus[y, i].OccupiedWhiteFields)
                {
                    KingAttacking(king, figure, y, i);
                    break;
                }
            }
        }
        // UpLeft
        // UpRight
        // ...
        public static bool UpMovement(int oldY, int x, int y, LiveGame liveGame)
        {
            for (int i = oldY - 1; i > y; i--)
            {
                if (liveGame.FieldsStatus[i, x].OccupiedBlackFields || liveGame.FieldsStatus[i, x].OccupiedWhiteFields)
                {
                    return false;
                }
            }
            return true;
        }
        public static bool DownMovement(int oldY, int x, int y, LiveGame liveGame)
        {
            for (int i = oldY + 1; i < y; i++)
            {
                if (liveGame.FieldsStatus[i, x].OccupiedBlackFields || liveGame.FieldsStatus[i, x].OccupiedWhiteFields)
                {
                    return false;
                }
            }
            return true;
        }
        public static bool LeftMovement(int oldX, int x, int y, LiveGame liveGame)
        {
            for (int i = oldX - 1; i > x; i--)
            {
                if (liveGame.FieldsStatus[y, i].OccupiedBlackFields || liveGame.FieldsStatus[y, i].OccupiedWhiteFields)
                {
                    return false;
                }
            }
            return true;
        }
        public static bool RightMovement(int oldX, int x, int y, LiveGame liveGame)
        {
            for (int i = oldX + 1; i < x; i++)
            {
                if (liveGame.FieldsStatus[y, i].OccupiedBlackFields || liveGame.FieldsStatus[y, i].OccupiedWhiteFields)
                {
                    return false;
                }
            }
            return true;
        }
        // UpLeft
        // UpRight
        // ...
        public static bool CheckSetNewPosition(Figure figure, LiveGame liveGame, int x, int y, int oldY, int oldX,
            ISetNewAttackFieles setNewAttackFields, IFigureRepository figureRepository)
        {
            if (setNewAttackFields is null || figureRepository is null)
            {
                throw new Exception("Tego bledu tu nie powinno byc");
            }
            if(figure.Color == FigureColor.White)
            { 
                if (liveGame.FieldsStatus[y, x].OccupiedWhiteFields)
                {
                    throw new InvalidOperationException();
                }
                liveGame.FieldsStatus[oldY, oldX].OccupiedWhiteFields = false;
                var newAttackedBlackFields = setNewAttackFields.SetNewAttackFieles(liveGame, FigureColor.Black, null);
                var king = figureRepository.GetKing(liveGame, FigureColor.White);
                if (newAttackedBlackFields[king.Y, king.X])
                {
                    liveGame.FieldsStatus[oldY, oldX].OccupiedWhiteFields = true;
                    return false;
                }
                liveGame.FieldsStatus[oldY, oldX].OccupiedWhiteFields = true;
                return true;
                
            }
            else
            { 
                if (liveGame.FieldsStatus[y, x].OccupiedBlackFields)
                {
                    throw new InvalidOperationException();
                }
                liveGame.FieldsStatus[oldY, oldX].OccupiedBlackFields = false;
                var newAttackedWhiteFields = setNewAttackFields.SetNewAttackFieles(liveGame, FigureColor.White, null);
                var king = figureRepository.GetKing(liveGame, FigureColor.Black);
                if (newAttackedWhiteFields[king.Y, king.X])
                {
                    liveGame.FieldsStatus[oldY, oldX].OccupiedBlackFields = true;
                    return false;
                }
                liveGame.FieldsStatus[oldY, oldX].OccupiedBlackFields = true;
                return true;
            }
        }
        public static void SetNewPosition(Figure figure, LiveGame liveGame, int x, int y, IFigureRepository? figureRepository)
        {
            if (figureRepository is null)
            {
                throw new Exception("Tego bledu tu nie powinno byc");
            }
            if (figure.Color == FigureColor.White)
            {
                if (liveGame.FieldsStatus[y, x].OccupiedBlackFields)
                {
                    var toDeleteFigure = figureRepository.GetFigure(liveGame, y, x);
                    figureRepository.RemoveFigure(liveGame, toDeleteFigure);
                }
                liveGame.FieldsStatus[y, x].OccupiedWhiteFields = true;
                figure.X = x;
                figure.Y = y;
            }
            else
            {
                if (liveGame.FieldsStatus[y, x].OccupiedWhiteFields)
                {
                    var toDeleteFigure = figureRepository.GetFigure(liveGame, y, x);
                    figureRepository.RemoveFigure(liveGame, toDeleteFigure);
                }
                liveGame.FieldsStatus[y, x].OccupiedBlackFields = true;
                figure.X = x;
                figure.Y = y;
            }
        }
        public static bool UpAttack(int x, int y, int targetY, IEnumerable<Figure> defendingFigures, LiveGame liveGame,
            IFigureTypeMoveStrategySelector figureTypeMoveStrategySelector, IFigureRepository figureRepository, ISetNewAttackFieles setNewAttackFields)
        {
            for (int i = y; i > targetY; i--)
            {
                if (CheckCover(x, i, defendingFigures, liveGame, figureTypeMoveStrategySelector, 
                    figureRepository, setNewAttackFields))
                {
                    return true;
                }
            }
            return false;
        }
        public static bool DownAttack(int x, int y, int targetY, IEnumerable<Figure> defendingFigures, LiveGame liveGame,
            IFigureTypeMoveStrategySelector figureTypeMoveStrategySelector, IFigureRepository figureRepository, ISetNewAttackFieles setNewAttackFields)
        {
            for (int i = y; i < targetY; i++)
            {
                if (CheckCover(x, i, defendingFigures, liveGame, figureTypeMoveStrategySelector, 
                    figureRepository, setNewAttackFields))
                {
                    return true;
                }
            }
            return false;
        }
        public static bool LeftAttack(int x, int y, int targetX, IEnumerable<Figure> defendingFigures, LiveGame liveGame,
            IFigureTypeMoveStrategySelector figureTypeMoveStrategySelector, IFigureRepository figureRepository, ISetNewAttackFieles setNewAttackFields)
        {
            for (int i = x; i > targetX; i--)
            {
                if (CheckCover(i, y, defendingFigures, liveGame, figureTypeMoveStrategySelector, 
                    figureRepository, setNewAttackFields))
                {
                    return true;
                }
            }
            return false;
        }
        public static bool RightAttack(int x, int y, int targetX, IEnumerable<Figure> defendingFigures, LiveGame liveGame,
            IFigureTypeMoveStrategySelector figureTypeMoveStrategySelector, IFigureRepository figureRepository, ISetNewAttackFieles setNewAttackFields)
        {
            for (int i = x; i < targetX; i++)
            {
                if (CheckCover(i, y, defendingFigures, liveGame, figureTypeMoveStrategySelector, 
                    figureRepository, setNewAttackFields))
                {
                    return true;
                }
            }
            return false;
        }
        // UpDefend
        // DownDefend ...
        private static bool CheckCover(int x, int y, IEnumerable<Figure> defendingFigures, LiveGame liveGame, IFigureTypeMoveStrategySelector figureTypeMoveStrategySelector, IFigureRepository figureRepository, ISetNewAttackFieles setNewAttackFields)
        {
            foreach(var f in defendingFigures)
            {
                var figureMoveStrategy = figureTypeMoveStrategySelector.SelectMoveStrategy(f, null, null);
                if(figureMoveStrategy.CheckLegalMoveDirection(f.X, f.Y, x, y))
                {
                    var direction = figureMoveStrategy.SetDirection(f.X, f.Y, x, y);
                    if(figureMoveStrategy.ChcekLegalMovement(f, liveGame, f.X, f.Y, x, y, direction) || 
                        CheckSetNewPosition(f, liveGame, x, y, f.Y, f.X, setNewAttackFields, figureRepository))
                    {
                        return true;
                    }
                }
            }
            return false;
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
