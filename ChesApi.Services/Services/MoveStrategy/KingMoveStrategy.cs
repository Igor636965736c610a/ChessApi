using ChesApi.Infrastructure.Services.EnumFiguresDirection;
using ChesApi.Infrastructure.Services.MoveStrategy.MoveDirectionStrategy.SelectLogic;
using Chess.Core.Domain;
using Chess.Core.Domain.EnumsAndStructs;
using Chess.Core.Domain.Figures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChesApi.Infrastructure.Services.MoveStrategy
{
    public class KingMoveStrategy : IFigureTypeMoveStrategy
    {
        public bool ChcekLegalMovement(Figure figure, FieldsStatus[,] fieldsStatus, Vector2 oldVector2, Vector2 newVector2, EnumDirection enumDirection)
        {
            throw new NotImplementedException();
        }

        public bool CheckCheckMate(Vector2 Vector2, Figure king, IEnumerable<Figure> defendingFigures,
            LiveGame liveGame, EnumDirection direction, IFigureTypeMoveStrategySelector figureTypeMoveStrategySelector)
        {
            throw new NotImplementedException();
        }

        public bool CheckLegalMoveDirection(Vector2 oldVector2, Vector2 newVector2)
        {
            throw new NotImplementedException();
        }

        public void Move(Figure figure, LiveGame liveGame, Vector2 oldVector2, Vector2 newVector2, EnumDirection enumDirection)
        {
            throw new NotImplementedException();
        }

        public void SetAttackFieles(FieldsStatus[,] fieldsStatus, bool[,] newAttackedFields, Figure figure, Figure? king)
        {
            throw new NotImplementedException();
        }

        public EnumDirection SetDirection(Vector2 oldVector2, Vector2 newVector2)
        {
            throw new NotImplementedException();
        }
    }
}
