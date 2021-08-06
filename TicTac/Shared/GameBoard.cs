using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTac.Shared
{
    public class GameBoard
    {
        public PieceStyle[,] Board { get; set; }
        public bool IsFinished => IsCompleted() || IsDraw();

        public GameBoard()
        {
            Reset();
        }

        public void Reset()
        {
            Board = new PieceStyle[3, 3];

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Board[i, j] = PieceStyle.Blank;
                }
            }
        }

        public bool PieceClicked(int x, int y, PieceStyle style)
        {
            if (Board[x, y] == PieceStyle.Blank)
            {
                Board[x, y] = style;
                return true;
            }
            return false;
        }

        public PieceStyle WinnerPiece()
        {
            return PieceStyle.Blank;
        }

        private bool IsDraw()
        {
            return false;
        }

        private bool IsCompleted()
        {
            return false;
        }
    }
}
