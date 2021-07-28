using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTac.Shared
{
    public enum PieceStyle
    {
        X,
        O,
        Blank
    }

    public class GamePiece
    {
        public PieceStyle Style;
        public GamePiece()
        {
            Style = PieceStyle.Blank;
        }
    }
}
