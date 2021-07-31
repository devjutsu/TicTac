using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTac.Server.App.TicTacToe
{
    public interface ITicTacHub
    {
        Task StartGame();
    }
}
