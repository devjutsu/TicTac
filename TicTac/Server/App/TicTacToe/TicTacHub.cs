using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace TicTac.Server.App.TicTacToe
{
    public interface ITicTacHub
    {
        Task Register(string userId, string userName);
        Task Start(string x, string o);
    }

    public class TicTacHub : Hub, ITicTacHub
    {
        readonly IHubContext<TicTacHub> _ctx;

        public TicTacHub(IHubContext<TicTacHub> ctx)
        {
            _ctx = ctx;
        }

        public async Task Register(string userId, string userName)
        {
            await _ctx.Clients.All.SendAsync("Register", $"{userName} is ready");
        }

        public async Task Start(string x, string o)
        {
            await _ctx.Clients.All.SendAsync("GameStart", x, o);
        }

        public override async Task OnConnectedAsync()
        {
            Console.WriteLine($"{Context.ConnectionId} connected");
            await Clients.All.SendAsync("broadcastMessage", "system", $"{Context.ConnectionId} joined the conversation");
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(System.Exception e)
        {
            Console.WriteLine($"Disconnected {e?.Message} {Context.ConnectionId}");
            await Clients.All.SendAsync("broadcastMessage", "system", $"{Context.ConnectionId} left the conversation");
            await base.OnDisconnectedAsync(e);
        }
    }
}
