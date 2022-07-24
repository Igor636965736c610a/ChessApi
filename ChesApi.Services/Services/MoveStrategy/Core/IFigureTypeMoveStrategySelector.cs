﻿using Chess.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChesApi.Services.Services.MoveStrategy.MoveDirectionStrategy.Core
{
    public interface IFigureTypeMoveStrategySelector
    {
        IFigureTypeMoveStrategy SelectMoveStrategy(Figure figure);
    }
}
