using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTac.Shared
{
    public record PieceClick
    {
        public string Player { get; init; }
        public PieceStyle Style { get; init; }
        public int X { get; init; }
        public int Y { get; init; }
    }
}
