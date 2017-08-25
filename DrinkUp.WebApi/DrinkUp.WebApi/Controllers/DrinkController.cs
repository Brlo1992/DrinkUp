using System.Collections.Generic;
using DrinkUp.WebApi.Services;
using DrinkUp.WebApi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using DrinkUp.WebApi.Model.Service;

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
        public async Task<IActionResult> Get() {
            var result = await drinkService.GetAll();
            return responseService.GetResponse(result);
        }

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

        [HttpPost]
        [Route("addSamples")]
        public async Task<IActionResult> Post(long password) {
            var drinks = GetTestDrinks();
            var result = new ServiceResult();
            if (password == 929503041110) {
                foreach (var viewModel in drinks) {
                    result = await drinkService.Add(viewModel);
                    if (result.IsValid == false)
                        break;
                }
            }
            return responseService.GetResponse(result);
        }

        private IEnumerable<DrinkViewModel> GetTestDrinks() {
            return new List<DrinkViewModel> {
                new DrinkViewModel {
                    Name = "Sex on the beach",
                    Description =
                        @"Do wype�nionej lodem szklanicy barma�skiej wla� sk�adniki nie uwzgl�dniaj�c soku �urawinowego. Wlane sk�adniki wstrz�sn�� w shakerze przez ok 10s sek. Nast�pnie zawarto�� przela� do uprzednio przygotowanej szklanki typu highball, b�d� te� fancy wype�nionej do po�owy lodem. Nast�pnie dola� tzw. splash, kt�rym w tym momencie jest sok �urawinowy. Ozdobi� drinka.W przypadku nie posiadania soku �urawinowego, jako zast�pnik u�ywa si� soku z czarnej porzeczki, b�d� te� syropu grenadine. Cz�sto u�ywa si� syrop�w zamiast likier�w, przez co drink jest jeszcze s�odszy lecz w klasycznym wykonaniu drink powinien by� na bazie likier�w",
                    Glass = "Highball",
                    Ingredients = new List<string> {
                        "w�dka - 40 ml ",
                        "likier kokosowy - 20 ml",
                        "likier brzoskwiniowy - 20 ml",
                        "pomara�cza - kawa�ek do dekoracji",
                        "sok �urawinowy (splash) - 40 ml",
                        "sok pomara�czowy - 40 ml",
                        "l�d - kilka kostek"
                    }
                },
                new DrinkViewModel {
                    Name = "Black Russian ",
                    Description =
                        @"Przygotowanie Black Russian nie powinno przysporzy� wi�kszych trudno�ci ani przy zakupie sk�adnik�w, ani przy samym przyrz�dzaniu drinka. Wystarczy wype�ni� tumbler na lodzie, kt�ry jednocze�nie b�dzie s�u�y� jako naczynie do podania drinka, do po�owy kostkami lodu. Nast�pnie nale�y wla� 1,5 miarki w�dki i 0,75 miarki likieru kawowego. Potem wystarczy ju� tylko bardzo delikatnie zamiesza� drinka. Black Russian widnieje na li�cie przepis�w IBA.",
                    Glass = "Tumbler",
                    Ingredients = new List<string> {
                        "w�dka - 100 ml ",
                        "kalhua  - 50 ml",
                        "l�d - kilka kostek"
                    }
                },
                new DrinkViewModel {
                    Name = "Black Russian ",
                    Description =
                        @"W�dk� i likier oraz kostki lodu umie�ci� w szklanicy barma�skiej i zamiesza�. Przela� do kieliszka koktajlowego. Na powierzchni po�o�y� lekko ubit� �mietan�. Nap�j pije si� te� z kostkami lodu. W�wczas podaje si� go w ma�ej szklance typu tumbler.",
                    Glass = "Tumbler",
                    Ingredients = new List<string> {
                        "w�dka - 100 ml ",
                        "kalhua  - 50 ml",
                        "l�d - kilka kostek",
                        "skondensowane mleczko - 30 ml"
                    }
                },
                new DrinkViewModel {
                    Name = "Black Russian ",
                    Description =
                        @"Wlej rum,niebieskie curacao,sok z ananasa i krem kokosowy do blendera razem ze szklank� pokruszonego lodu. Miksuj przez 15 sekund lub do momentu uzyskania pienistej masy. Wlej do najdziwniejszego kieliszka, jaki znajdziesz i udekoruj plasterkiem ananasa oraz wisienka koktajlow�.",
                    Glass = "Tumbler",
                    Ingredients = new List<string> {
                        "bia�y rum - 30 ml ",
                        "curacao - 30 m",
                        "sok ananasowy - 60 ml",
                        "krem kokosowy - 30 ml",
                        "ananas - plasterek",
                        "wisienka - dla dekoracji"
                    }
                }
            };
        }
    }
}