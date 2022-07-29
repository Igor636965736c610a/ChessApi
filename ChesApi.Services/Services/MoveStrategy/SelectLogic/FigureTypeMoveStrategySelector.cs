using ChesApi.Infrastructure.Services.AttackedFiels;
using Chess.Core.Domain;
using Chess.Core.Domain.Enums;
using Chess.Core.Repo.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChesApi.Infrastructure.Services.MoveStrategy.MoveDirectionStrategy.SelectLogic
{
    public class FigureTypeMoveStrategySelector : IFigureTypeMoveStrategySelector
    {
        public IFigureTypeMoveStrategy SelectMoveStrategy
            (Figure figure, IFigureRepository? figureRepository, ISetNewAttackFieles? setNewAttackFieles) => figure.FigureType switch
        {
            FigureType.Queen => new QueenMoveStrategy(),
            FigureType.King => new KingMoveStrategy(),
            FigureType.Knight => new KnightMoveStrategy(),
            FigureType.Bishop => new BishopMoveStrategy(),
            FigureType.Pown => new PownMoveStrategy(),
            FigureType.Rock => new RockMoveStrategy(figureRepository, setNewAttackFieles),
            _ => throw new InvalidOperationException(),
        };
    }
}
