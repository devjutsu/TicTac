using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using static System.Net.Mime.MediaTypeNames;

namespace TicTac.Server.App.TicTacToe
{
    public interface ITicTacHub
    {
        Task Register(string userConnectionId, string userId, string userName);
        Task Start(string x, string o);
    }

    public class TicTacHub : Hub, ITicTacHub
    {
        readonly IHubContext<TicTacHub> _ctx;
        public List<(string userId, string connectionId)> _clients { get; set; }

        public TicTacHub(IHubContext<TicTacHub> ctx)
        {
            _ctx = ctx;
            _clients = new List<(string userId,string connectionId)>();
        }

        public async Task Register(string userConnectionId, string userId, string userName)
        {
            await _ctx.Clients.Client(userConnectionId).SendAsync("Register", $"{userName} is ready");

            if (!_clients.Contains((userId, userConnectionId)))
                _clients.Add((userId, userConnectionId));
        }

        public async Task Start(string x, string o)
        {
            var xId = _clients.Where(u => u.userId == x).First();
            var oId = _clients.Where(u => u.userId == o).First();

            //await _ctx.Clients.Client(xId.connectionId).SendAsync("GameStart", x, o);
            //await _ctx.Clients.Client(oId.connectionId).SendAsync("GameStart", x, o);

            await _ctx.Clients.All.SendAsync("GameStart", x, o);
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
