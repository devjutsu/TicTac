using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicTac.Shared;

namespace TicTac.Server.App.TicTacToe
{
    public interface IToeService
    {
        void RegisterInGame(string userName);
        void StartGame(string userId, string opponentName);
        Task Move(PieceClick click);
    }

    public class ToeService : IToeService
    {
        private List<string> _queue;
        private List<List<string>> _activeGames;
        private readonly ITicTacHub _hub;
        private readonly Random _random;

        public ToeService(ITicTacHub hub)
        {
            _queue = new List<string>();
            _activeGames = new List<List<string>>();
            _hub = hub;
            _random = new Random();
        }

        public void RegisterInGame(string userName)
        {
            var opponentId = _queue.Where(o => o != userName).FirstOrDefault();
            if (opponentId == null)
                _queue.Add(userName);
            else
            {
                StartGame(userName, opponentId);
                _queue.Remove(opponentId);
            }
        }

        public void StartGame(string userName, string opponentName)
        {
            _activeGames.Add(new List<string>() { userName, opponentName });

            if (_random.Next(0, 2) != 0)
                _hub.Start(userName, opponentName);
            else
                _hub.Start(opponentName, userName);
        }

        public async Task Move(PieceClick click)
        {
            var opponent = _activeGames.First(o => o.Contains(click.Player))
                                        .First(o => o != click.Player);

            await _hub.NotifyMove(opponent, click);
        }
    }
}
