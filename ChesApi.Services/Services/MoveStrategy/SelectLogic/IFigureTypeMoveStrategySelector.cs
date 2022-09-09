using ChesApi.Infrastructure.Services.AttackedFiels;
using Chess.Core.Domain.Figures;
using Chess.Core.Repo.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChesApi.Infrastructure.Services.MoveStrategy.MoveDirectionStrategy.SelectLogic
{
    public interface IFigureTypeMoveStrategySelector
    {
        IFigureTypeMoveStrategy SelectMoveStrategy(Figure figure, IFigureRepository figureRepository);
    }
}
