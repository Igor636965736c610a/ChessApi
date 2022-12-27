using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChesApi.Infrastructure.DTO
{
    public class WaitingPlayersDTO
    {
        public string Name { get; private set; }
        public string ConntectionId { get; private set; }
        public bool WhiteColor { get; set; }
    }
}
