using DrinkUp.WebApi.Services;
using DrinkUp.WebApi.Utils;
using DrinkUp.WebApi.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DrinkUp.WebApi.Controllers {
    [Produces("application/json")]
    [Route("api/Home")]
    public class HomeController : Controller {
        private readonly ISearchService searchService;
        private readonly IResponseService responseService;

        public HomeController(ISearchService searchService,
            IResponseService responseService) {
            this.responseService = responseService;
            this.searchService = searchService;
        }

        [HttpGet]
        public IActionResult Get(SearchViewModel viewModel = null) => 
            responseService.GetResponse(searchService.Search(viewModel));
    }
}