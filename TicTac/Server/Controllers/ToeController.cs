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

        public ToeController(ILogger<ToeController> logger,
                                UserManager<ApplicationUser> userManager,
                                SignInManager<ApplicationUser> signInManager,
                                ITicTacHub hub)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _hub = hub;
        }

        [HttpGet("get")]
        public async Task<Psst> Get()
        {
            _logger.LogInformation("Got it.");

            //var id = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            //var name = (await _userManager.FindByIdAsync(id)).UserName;

            //var user = await _userManager.GetUserAsync(HttpContext.User);
            //var bz = user?.Email;

            return new Psst() { Name = "sampl" };
        }

        [HttpGet("fire")]
        public async Task Fire()
        {
            _logger.LogInformation("Start Game.");
            await _hub.StartGame();
        }

        public void ReadyToStart()
        {
            // check if 2 players are ready
            // if not ready, return to wait
            // if both players ready, create and return game instance
        }
    }
}
