using ChesApi.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChesApi.Infrastructure.Services
{
    public interface IGameService
    {
        LiveGameDTO GetGame(string connectionId);
        PlayerDTO GetPlayer(string connectionId);
    }
}
