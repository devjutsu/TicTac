using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicTac.Server.App.TicTacToe
{
    public interface IToeService
    {
        void RegisterInGame(string userId);
        void StartGame(string userId, string opponentId);

    }

    public class ToeService : IToeService
    {
        private List<string> _queue;
        private readonly ITicTacHub _hub;
        private readonly Random _random;

        public ToeService(ITicTacHub hub)
        {
            _queue = new List<string>();
            _hub = hub;
            _random = new Random();
        }

        public void RegisterInGame(string userId)
        {
            _queue.Remove(userId);
            var opponentId = _queue.FirstOrDefault();
            _queue.Remove(opponentId);
            if(opponentId != null)
            {
                StartGame(userId, opponentId);
            }
        }

        public void StartGame(string userId, string opponentId)
        {
            if (_random.Next(0, 2) != 0)
                _hub.Start(userId, opponentId);
            else
                _hub.Start(opponentId, userId);
        }

    }

    public class ToePlayers
    {
        public string X { get; set; }
        public string O { get; set; }
    }
}
