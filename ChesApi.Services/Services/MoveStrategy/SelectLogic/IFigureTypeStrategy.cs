using ChesApi.Infrastructure.Services.EnumFiguresDirection;
using Chess.Core.Domain;
using Chess.Core.Domain.Enums;
using Chess.Core.Domain.EnumsAndStructs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChesApi.Infrastructure.Services.MoveStrategy.MoveDirectionStrategy.SelectLogic
{
    public interface IFigureTypeMoveStrategy
    {
        void Move(Figure figure, LiveGame liveGame, int oldX, int oldY, int newX, int newY, EnumDirection enumDirection);
        EnumDirection SetDirection(int oldX, int oldY, int newX, int newY);
        void SetAttackFieles(FielsStatus[,] fielsStatus, bool[,] newAttackedFiels, Figure figure, Figure? king);

    }
}
