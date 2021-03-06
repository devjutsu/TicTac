using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicTac.Shared;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using TicTac.Server.Models;
using Microsoft.AspNetCore.Components.Authorization;
using TicTac.Server.App.TicTacToe;
using System.Net.Http;

namespace TicTac.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ToeController : ControllerBase
    {
        private readonly ILogger<ToeController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ITicTacHub _hub;
        private readonly IToeService _game;

        public ToeController(ILogger<ToeController> logger,
                                UserManager<ApplicationUser> userManager,
                                SignInManager<ApplicationUser> signInManager,
                                ITicTacHub hub,
                                IToeService game)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _hub = hub;
            _game = game;
        }

        [HttpGet("register/{id}")]
        public async Task Register(string id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var userName = (await _userManager.FindByIdAsync(userId)).UserName;
            _logger.LogInformation($"toe/register/{id} for {userName} ({userId}) ");

            await _hub.Register(id, userId, userName);
            _game.RegisterInGame(userName);
        }

        [HttpPost("move")]
        public async Task<HttpResponseMessage> Move(PieceClick click)
        {
            _logger.LogInformation($"{click.Player} ({click.Style}) clicked ({click.X}, {click.Y})");
            await _game.Move(click);
            return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
        }
    }

    
}
