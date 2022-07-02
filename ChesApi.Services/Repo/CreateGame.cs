using Chess.Core.Domain;
using Chess.Core.Domain.DefaultConst;
using Chess.Core.Domain.Enums;
using Chess.Core.Repo.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChesApi.Services.Repo
{
    public class CreateGame : ICreateGameRepository
    {
        public List<LiveGame> Game = new List<LiveGame>();
        public void CreategGame()
        {
            Game.Add(new LiveGame
                (Default.Figures, Default.DefaultWhiteAttackedFieles, Default.DefaultBlackAttackedFieles, 
                 Default.DefaultWhiteOccupiedFieles, Default.DefaultBlackOccupiedFieles));
        }
    }
}
