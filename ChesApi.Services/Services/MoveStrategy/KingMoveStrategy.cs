using ChesApi.Infrastructure.Services.EnumFiguresDirection;
using ChesApi.Infrastructure.Services.MoveStrategy.MoveDirectionStrategy.SelectLogic;
using Chess.Core.Domain;
using Chess.Core.Domain.EnumsAndStructs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChesApi.Infrastructure.Services.MoveStrategy
{
    public class KingMoveStrategy : IFigureTypeMoveStrategy
    {
        public bool ChcekLegalMovement(Figure figure, LiveGame liveGame, int oldX, int oldY, int newX, int newY, EnumDirection enumDirection)
        {
            throw new NotImplementedException();
        }

        public bool CheckCheckMate(int x, int y, Figure king, IEnumerable<Figure> defendingFigures, LiveGame liveGame, EnumDirection direction, IFigureTypeMoveStrategySelector figureTypeMoveStrategySelector)
        {
            throw new NotImplementedException();
        }

        public bool CheckLegalMoveDirection(int oldX, int oldY, int newX, int newY)
        {
            throw new NotImplementedException();
        }

        public void Move(Figure figure, LiveGame liveGame, int oldX, int oldY, int newX, int newY, EnumDirection enumDirection)
        {
            throw new NotImplementedException();
        }

        public void SetAttackFieles(FieldsStatus[,] fieldsStatus, bool[,] newAttackedFields, Figure figure, Figure? king)
        {
            throw new NotImplementedException();
        }

        public EnumDirection SetDirection(int oldX, int oldY, int newX, int newY)
        {
            throw new NotImplementedException();
        }
    }
}
