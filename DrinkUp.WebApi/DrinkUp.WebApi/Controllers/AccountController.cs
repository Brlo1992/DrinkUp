﻿using System.Threading.Tasks;
using DrinkUp.WebApi.Services;
using DrinkUp.WebApi.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DrinkUp.WebApi.Controllers {
    [Produces("application/json")]
    [Route("api/account")]
    public class AccountController : Controller {
        private IAccountService accountService;
        private IResponseService responseService;

        public AccountController(IAccountService accountService, IResponseService responseService) {
            this.accountService = accountService;
            this.responseService = responseService;
        }
        [AllowAnonymous]
        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> LogIn(LoginViewModel viewModel) {
            var result = await accountService.LogIn(viewModel);
            return responseService.GetResponse(result);
        }

        [AllowAnonymous]
        [Route("register")]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel viewModel) {
            var result = await accountService.Register(viewModel);
            return responseService.GetResponse(result);
        }

        [Authorize]
        [Route("logout")]
        [HttpPost]
        public async Task<IActionResult> LogOut() {
            var result = await
            return responseService.GetResponse(result);
        }
    }
}