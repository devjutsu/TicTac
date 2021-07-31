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
            await _ctx.Clients.All.SendAsync("test");
        }

        public override Task OnConnectedAsync()
        {
            Clients.All.SendAsync("broadcastMessage", "system", $"{Context.ConnectionId} joined the conversation");
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(System.Exception exception)
        {
            Clients.All.SendAsync("broadcastMessage", "system", $"{Context.ConnectionId} left the conversation");
            return base.OnDisconnectedAsync(exception);
        }
    }
}
