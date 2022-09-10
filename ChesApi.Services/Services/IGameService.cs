using Chess.Core.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChesApi.Infrastructure.Services
{
    public interface IGameService
    {
        Task CreateGame(FigureColor figureColour, Guid userId);
        Task JoinToTheGame(Guid gameId, Guid userId);
    }
}
