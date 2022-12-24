using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChesApi.Infrastructure.DTO
{
    public class GameCharDTO
    {
        public GameCharDTO()
        {
        }
        public GameCharDTO(char figureChar)
        {
            FigureChar = figureChar;
        }
        public bool WhiteColor { get; set; }
        public char FigureChar { get; set; }
    }
}
