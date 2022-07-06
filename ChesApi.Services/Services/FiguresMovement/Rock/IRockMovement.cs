using Chess.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChesApi.Services.Services.FiguresMovement.Rock
{
    public interface IRockMovement
    {
        void RockUpMovement(int oldX, int x, int y, Figure figure, LiveGame liveGame);
        void RockDownMovement(int oldX, int oldY, int x, int y, Figure figure, LiveGame liveGame);
        void RockRightMovement(int oldX, int oldY, int x, int y, Figure figure, LiveGame liveGame);
        void RockLeftMovement(int oldX, int oldY, int x, int y, Figure figure, LiveGame liveGame);
    }
}
