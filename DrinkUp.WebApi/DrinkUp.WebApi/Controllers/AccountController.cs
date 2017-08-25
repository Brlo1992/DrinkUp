using DrinkUp.WebApi.Services;
using DrinkUp.WebApi.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DrinkUp.WebApi.Controllers {
    [Produces("application/json")]
    [Route("api/account")]
    public class AccountController : Controller {
        private readonly IAccountService accountService;
        private readonly IResponseService responseService;

        public AccountController(IAccountService accountService, IResponseService responseService) {
            this.accountService = accountService;
            this.responseService = responseService;
        }
        [AllowAnonymous]
        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> LogIn([FromBody] LoginViewModel viewModel) {
            var result = await accountService.LogIn(viewModel);
            return responseService.GetResponse(result);
        }

        [AllowAnonymous]
        [Route("register")]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel viewModel) {
            var result = await accountService.Register(viewModel);
            return responseService.GetResponse(result);
        }

        [Authorize]
        [Route("logout")]
        [HttpPost]
        public async Task<IActionResult> LogOut() {
            var result = await accountService.LogOut();
            return responseService.GetResponse(result);
        }
    }
}