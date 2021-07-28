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

namespace TicTac.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ToeController : ControllerBase
    {
        private readonly ILogger<ToeController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public ToeController(ILogger<ToeController> logger, 
                                UserManager<ApplicationUser> userManager,
                                SignInManager<ApplicationUser> signInManager)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<Psst> Get()
        {
            var id = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var name = (await _userManager.FindByIdAsync(id)).UserName;

            var user = await _userManager.GetUserAsync(HttpContext.User);
            var bz = user?.Email;

            return new Psst() { Name = name };
        }
    }
}
