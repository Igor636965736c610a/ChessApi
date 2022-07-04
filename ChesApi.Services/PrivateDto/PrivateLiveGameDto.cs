using ChesApi.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChesApi.Services.PrivateDto
{
    public class PrivateLiveGameDto
    {
        public List<PrivateFigureDto> Figures = new List<PrivateFigureDto>();
        public bool StartGame { get; set; } = false;
        public bool IsGaming { get; set; } = false;
        public bool WhiteMat { get; set; } = false;
        public bool BlackMat { get; set; } = false;
        public bool[,] AttackedWhiteFiels { get; set; }
        public bool[,] AttackedBlackFiels { get; set; }
        public bool[,] OccupiedWhiteFieles { get; set; }
        public bool[,] OccupiedBlackFieles { get; set; }
        public Guid Id { get; set; }
        public PrivateUserDto User1 { get; set; }
        public PrivateUserDto User2 { get; set; }
    }
}
