using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChesApi.Infrastructure.DTO
{
    public class PlayerDTO
    {
        public string Name { get; set; }
        public bool HasMove { get; set; }
        public bool WhiteColor { get; set; }
    }
}
