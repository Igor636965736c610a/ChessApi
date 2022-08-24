using Chess.Core.Domain;
using Chess.Core.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChesApi.Infrastructure.Services.AttackedFiels
{
    public interface ISetNewAttackFields
    {
        bool[,] SetNewAttackFieles(LiveGame liveGame, FigureColor figureColor, Figure? king);
    }
}
