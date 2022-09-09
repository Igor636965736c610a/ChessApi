using ChesApi.Infrastructure.Services.AttackedFiels;
using Chess.Core.Domain.Enums;
using Chess.Core.Domain.Figures;
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
            (Figure figure, IFigureRepository figureRepository) => figure.FigureType switch
        {
            FigureType.Queen => new QueenMoveStrategy(figureRepository),
            FigureType.King => new KingMoveStrategy(),
            FigureType.Knight => new KnightMoveStrategy(),
            FigureType.Bishop => new BishopMoveStrategy(figureRepository),
            FigureType.Pown => new PownMoveStrategy(),
            FigureType.Rook => new RockMoveStrategy(figureRepository),
            _ => throw new InvalidOperationException(),
        };
    }
}
