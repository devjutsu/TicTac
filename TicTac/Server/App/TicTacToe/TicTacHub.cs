using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using TicTac.Shared;
using static System.Net.Mime.MediaTypeNames;

namespace TicTac.Server.App.TicTacToe
{
    public interface ITicTacHub
    {
        Task Register(string userConnectionId, string userId, string userName);
        Task Start(string x, string o);
        Task NotifyMove(string opponent, PieceClick click);
    }

    public class TicTacHub : Hub, ITicTacHub
    {
        readonly IHubContext<TicTacHub> _ctx;
        public List<(string userName, string connectionId)> _clients { get; set; }

        public TicTacHub(IHubContext<TicTacHub> ctx)
        {
            _ctx = ctx;
            _clients = new List<(string userName, string connectionId)>();
        }

        public async Task Register(string userConnectionId, string userId, string userName)
        {
            await _ctx.Clients.Client(userConnectionId).SendAsync("Register", $"{userName} is ready");

            if (!_clients.Contains((userName, userConnectionId)))
            {
                _clients.RemoveAll(o => o.userName == userName);
                _clients.Add((userName, userConnectionId));
            }
        }

        public async Task Start(string xPlayer, string oPlayer)
        {
            var xId = _clients.Where(u => u.userName == xPlayer).First();
            await _ctx.Clients.Client(xId.connectionId).SendAsync("GameStart", xPlayer, oPlayer, true);

            var oId = _clients.Where(u => u.userName == oPlayer).First();
            await _ctx.Clients.Client(oId.connectionId).SendAsync("GameStart", oPlayer, xPlayer, false);
        }

        public async Task NotifyMove(string opponent, PieceClick click)
        {
            var connectionId = _clients.First(o => o.userName == opponent).connectionId;

            await _ctx.Clients.Client(connectionId).SendAsync("NotifyMove", click);
        }


        public override async Task OnConnectedAsync()
        {
            Console.WriteLine($"{Context.ConnectionId} connected");

            await Clients.Client(Context.ConnectionId).SendAsync("broadcastMessage", "system", $"{Context.ConnectionId} joined");
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(System.Exception e)
        {
            Console.WriteLine($"Disconnected {e?.Message} {Context.ConnectionId}");
            var itemToRemove = _clients.FirstOrDefault(o => o.connectionId == Context.ConnectionId);
            _clients.Remove(itemToRemove);
            await base.OnDisconnectedAsync(e);
        }
    }
}
