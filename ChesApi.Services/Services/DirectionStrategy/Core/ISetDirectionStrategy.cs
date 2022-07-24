using ChesApi.Services.Services.EnumFiguresDirection;
using Chess.Core.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChesApi.Services.Services.DirectionStrategy.Core
{
    public interface ISetDirectionStrategy
    {
        EnumDirection SetDirection(FigureType figureType);
    }
}
