using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Core.Domain
{
    public class Player
    {
        public Guid UserId { get; private set; }
        public Guid Id { get; private set; } = Guid.NewGuid();
        public Guid GameId { get; internal set; }
        public string Name { get; private set; }
        public string ConnectionId { get; private set; }
        public bool HasMove { get; set; }
        public bool WhiteColor { get; set; }

        public Player(Guid userId, string name, string conntectionId, bool hasMove, bool whiteColor)
        {
            UserId = userId;
            Name = name;
            ConnectionId = conntectionId;
            HasMove = hasMove;
            WhiteColor = whiteColor;
        }
    }
}
