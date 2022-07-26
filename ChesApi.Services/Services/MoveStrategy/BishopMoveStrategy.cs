using ChesApi.Infrastructure.Services.EnumFiguresDirection;
using ChesApi.Infrastructure.Services.MoveStrategy.MoveDirectionStrategy.SelectLogic;
using Chess.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChesApi.Infrastructure.Services.MoveStrategy
{
    public class BishopMoveStrategy : IFigureTypeMoveStrategy
    {
        public void Move(Figure figure, LiveGame liveGame, int oldX, int oldY, int newX, int newY, EnumDirection enumDirection)
        {
            throw new NotImplementedException();
        }

        public EnumDirection SetDirection(int oldX, int oldY, int newX, int newY)
        {
            throw new NotImplementedException();
        }
    }
}
