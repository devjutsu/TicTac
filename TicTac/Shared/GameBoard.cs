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
        private bool IsCompleted()
            => WinnerPiece() != PieceStyle.Blank;
        public PieceStyle Winner { get; set; }

        public GameBoard()
        {
            Reset();
        }

        public void Reset()
        {
            Board = new PieceStyle[3, 3];
            Winner = PieceStyle.Blank;

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
            for (int i = 0; i < 3; i++)
            {
                var res = CheckLine(Board[i, 0], Board[i, 1], Board[i, 2]);
                if (res != PieceStyle.Blank)
                    return res;
            }

            for (int j = 0; j < 3; j++)
            {
                var res = CheckLine(Board[0, j], Board[1, j], Board[2, j]);
                if (res != PieceStyle.Blank)
                    return res;
            }

            var style = CheckLine(Board[0, 0], Board[1, 1], Board[2, 2]);
            if (style != PieceStyle.Blank)
                return style;
            style = CheckLine(Board[2, 0], Board[1, 1], Board[0, 2]);
            if (style != PieceStyle.Blank)
                return style;

            return PieceStyle.Blank;
        }


        public PieceStyle CheckLine(PieceStyle a, PieceStyle b, PieceStyle c)
        {
            if (a == b && b == c)
            {
                Winner = a;
                return a;
            }

            return PieceStyle.Blank;
        }

        private bool IsDraw()
        {
            foreach (PieceStyle style in Board)
            {
                if (style == PieceStyle.Blank)
                    return false;
            }
            return true;
        }
    }
}
