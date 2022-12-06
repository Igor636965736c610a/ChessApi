using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Core.Domain
{
    public class Player
    {
        public Guid UserId { get; set; }
        public Guid Id { get; set; } = new Guid();
        public Guid GameId { get; set; }
        public string Name { get; set; }
        public string ConntectionId { get; set; }
        public bool HasMove { get; set; }
        public bool WhiteColor { get; set; }

        public Player(Guid userId, string name, string conntectionId, bool hasMove, bool whiteColor)
        {
            UserId = userId;
            Name = name;
            ConntectionId = conntectionId;
            HasMove = hasMove;
            WhiteColor = whiteColor;
        }
    }
}
