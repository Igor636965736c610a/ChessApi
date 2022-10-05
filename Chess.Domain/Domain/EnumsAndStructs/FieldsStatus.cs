﻿using Chess.Core.Domain.Figures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Core.Domain.EnumsAndStructs
{
    public struct FieldsStatus
    {
        public bool CanEnPassant { get; set; }
        public Figure? Figure { get; set; }
    }
}
