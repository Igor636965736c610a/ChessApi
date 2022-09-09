using Chess.Core.Domain.Figures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Core.Domain.EnumsAndStructs
{
    public struct FieldsStatus
    {
        public bool AttackedWhiteFields { get; set; }
        public bool AttackedBlackFields { get; set; }
        public bool OccupiedWhiteFields { get; set; }
        public bool OccupiedBlackFields { get; set; }
        public Figure? Figure { get; set; }
    }
}
