using System.Threading.Tasks;
using DrinkUp.WebApi.Services;
using DrinkUp.WebApi.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DrinkUp.WebApi.Controllers {
    [Produces("application/json")]
    
    public class RateController : Controller
    {
        private readonly IRateService rateService;
        private readonly IResponseService responseService;


        public RateController(IRateService rateService, IResponseService responseService) {
            this.rateService = rateService;
            this.responseService = responseService;
        }

        [Route("api/rate/like")]
        [HttpPost]
        public async Task<IActionResult> Like([FromBody] IdentityViewModel viewModel) {
            var result = await rateService.RateDrink(viewModel, RateType.Like);

            return responseService.GetResponse(result);
        }

        [Route("api/rate/unlike")]
        [HttpPost]
        public async Task<IActionResult> Unlike([FromBody] IdentityViewModel viewModel) {
            var result = await rateService.RateDrink(viewModel, RateType.Unlike);

            return responseService.GetResponse(result);
        }
    }
}