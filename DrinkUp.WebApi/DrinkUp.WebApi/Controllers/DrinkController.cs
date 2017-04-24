using DrinkUp.WebApi.Services;
using DrinkUp.WebApi.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DrinkUp.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Drink")]
    public class DrinkController : Controller
    {
        private IDrinkService drinkService;

        public DrinkController(IDrinkService drinkService)
        {
            this.drinkService = drinkService;
        }

        [HttpPost]
        public IActionResult Post(DrinkViewModel drink)
        {
            drinkService.Add(drink);
            return Ok();
        }
    }
}