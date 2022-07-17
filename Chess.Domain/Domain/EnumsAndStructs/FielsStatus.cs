using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Core.Domain.EnumsAndStructs
{
    public struct FielsStatus
    {
        public bool AttackedWhiteFiels { get; set; }
        public bool AttackedBlackFiels { get; set; }
        public bool OccupiedWhiteFiels { get; set; }
        public bool OccupiedBlackFiels { get; set; }
    }
}
