using DrinkUp.WebApi.Services;
using DrinkUp.WebApi.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DrinkUp.WebApi.Controllers {
    [Produces("application/json")]
    [Route("api/Home")]
    public class HomeController : Controller {
        readonly ISearchService searchService;

        public HomeController(ISearchService searchService) {
            this.searchService = searchService;
        }

        [HttpGet]
        public IActionResult Get(SearchViewModel viewModel = null) {
            return Ok(new {
                Data = searchService.Search(viewModel)
            });
        }
    }
}