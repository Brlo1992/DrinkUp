using DrinkUp.WebApi.Services;
using DrinkUp.WebApi.Utils;
using DrinkUp.WebApi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DrinkUp.WebApi.Controllers {
    [Produces("application/json")]
    [Route("api/drink")]
    public class DrinkController : Controller {
        private readonly IDrinkService drinkService;
        private readonly IResponseService responseService;

        public DrinkController(IDrinkService drinkService,
            ISearchService searchService,
            IResponseService responseService) {
            this.drinkService = drinkService;
            this.responseService = responseService;
        }

        [HttpGet("{objectId}", Name = "GetById")]
        public async Task<IActionResult> Get(string objectId) {
            var result = await drinkService.GetSingle(new IdentityViewModel {
                Id = objectId
            });
            return responseService.GetResponse(result);
        }

        [HttpGet]
        public IActionResult Get() =>
            responseService.GetResponse(drinkService.GetAll());

        //Add one
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] DrinkViewModel viewModel) {
            var result = await drinkService.Add(viewModel);
            return responseService.GetResponse(result);
        }

        //Delete one
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] IdentityViewModel viewModel) {
            var result = await drinkService.Remove(viewModel);
            return responseService.GetResponse(result);
        }

        //Update one
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] DrinkViewModel viewModel) {
            var result = await drinkService.Update(viewModel);
            return responseService.GetResponse(result);
        }
    }
}