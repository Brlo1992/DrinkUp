using System.Linq;
using DrinkUp.WebApi.Model.Service;
using DrinkUp.WebApi.Services;
using DrinkUp.WebApi.Utils;
using DrinkUp.WebApi.ViewModels;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet]
        public IActionResult Get(SearchViewModel viewModel) =>
            responseService.GetResponse(drinkService.GetSingle(viewModel));

        [HttpGet]
        public IActionResult Get() =>
            responseService.GetResponse(drinkService.GetAll());

        //Add one
        [HttpPost]
        public IActionResult Post([FromBody] DrinkViewModel viewModel) =>
            responseService.GetResponse(drinkService.Add(viewModel));

        //Delete one
        [HttpDelete]
        public IActionResult Delete(IdentityViewModel viewModel) =>
            responseService.GetResponse(drinkService.Remove(viewModel));

        //Update one
        [HttpPut]
        public IActionResult Put()
    }
}