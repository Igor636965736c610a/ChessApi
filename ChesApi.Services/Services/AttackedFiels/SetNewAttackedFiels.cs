using Chess.Core.Domain.DefaultConst;
using Chess.Core.Domain;
using Chess.Core.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chess.Core.Domain.EnumsAndStructs;
using ChesApi.Infrastructure.Services.MoveStrategy.MoveDirectionStrategy.SelectLogic;

namespace ChesApi.Infrastructure.Services.AttackedFiels
{
    public class SetNewAttackedFieles : ISetNewAttackFields
    {
        private readonly IFigureTypeMoveStrategySelector _figureTypeMoveStrategySelector;
        public SetNewAttackedFieles(IFigureTypeMoveStrategySelector figureTypeMoveStrategySelector)
        {
            _figureTypeMoveStrategySelector = figureTypeMoveStrategySelector;
        }
        public bool[,] SetNewAttackFieles(LiveGame liveGame, FigureColor figureColor, Figure? king)
        {
            var figures = liveGame.Figures.Where(x => x.Color == figureColor);
            bool[,] newAttackFieles = new bool[Board.X, Board.Y];
            foreach (var f in figures)
            {
                var figureMoveStrategy = _figureTypeMoveStrategySelector.SelectMoveStrategy(f, null, null);
                figureMoveStrategy.SetAttackFieles(liveGame.FieldsStatus, newAttackFieles, f, king);
            }
            return newAttackFieles;
        }
    }
}