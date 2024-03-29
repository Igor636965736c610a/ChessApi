﻿using Chess.Core.Domain.Enums;
using Chess.Core.Domain.EnumsAndStructs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChesApi.Infrastructure.DTO
{
    public class FigureDTO
    {
        public Vector2 Vector2 { get; set; }
        public Value Value { get; set; }
        public bool WhiteColor { get; set; }
        public string FigureType { get; set; }
        public char FigureChar { get; set; }
    }
}
