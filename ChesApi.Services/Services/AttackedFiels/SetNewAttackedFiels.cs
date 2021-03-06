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
    public class SetNewAttackedFieles : ISetNewAttackFieles
    {
        private readonly IFigureTypeMoveStrategySelector _figureTypeMoveStrategySelector;
        public SetNewAttackedFieles(IFigureTypeMoveStrategySelector figureTypeMoveStrategySelector)
        {
            _figureTypeMoveStrategySelector = figureTypeMoveStrategySelector;
        }
        public bool[,] SetNewAttackFieles(LiveGame liveGame, FigureColour figureColor, Figure? king)
        {
            var figures = liveGame.Figures.Where(x => x.Colour == figureColor);
            bool[,] newAttackFieles = new bool[Board.Y, Board.X];
            foreach (var f in figures)
            {
                switch (f.FigureType)
                {
                    case FigureType.Queen:
                        {
                            break;
                        }
                    case FigureType.Pown:
                        {
                            break;
                        }
                    case FigureType.Bishop:
                        {
                            break;
                        }
                    case FigureType.Knight:
                        {
                            break;
                        }
                    case FigureType.Rock:
                        {
                            //strategy
                            var figureMoveStrategy = _figureTypeMoveStrategySelector.SelectMoveStrategy(f, null, null);
                            figureMoveStrategy.SetAttackFieles(liveGame.FielsStatus, newAttackFieles, f, king);
                            break;
                        }
                    case FigureType.King:
                        {
                            break;
                        }
                }
            }
            return newAttackFieles;
        }
    }
}