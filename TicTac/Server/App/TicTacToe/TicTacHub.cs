using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace TicTac.Server.App.TicTacToe
{
    public class TicTacHub : Hub, ITicTacHub
    {
        readonly IHubContext<TicTacHub> _ctx;

        public TicTacHub(IHubContext<TicTacHub> ctx)
        {
            _ctx = ctx;
        }

        public async Task StartGame()
        {
            await _ctx.Clients.All.SendAsync("StartGame", "my important message");
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
