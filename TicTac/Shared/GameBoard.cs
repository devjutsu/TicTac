using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTac.Shared
{
    public class GameBoard
    {

        public void PieceClicked(int x, int y)
        {
            Console.WriteLine($"{x}, {y}");
        }
    }
}
