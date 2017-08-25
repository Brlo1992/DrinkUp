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
                        @"Do wype³nionej lodem szklanicy barmañskiej wlaæ sk³adniki nie uwzglêdniaj¹c soku ¿urawinowego. Wlane sk³adniki wstrz¹sn¹æ w shakerze przez ok 10s sek. Nastêpnie zawartoœæ przelaæ do uprzednio przygotowanej szklanki typu highball, b¹dŸ te¿ fancy wype³nionej do po³owy lodem. Nastêpnie dolaæ tzw. splash, którym w tym momencie jest sok ¿urawinowy. Ozdobiæ drinka.W przypadku nie posiadania soku ¿urawinowego, jako zastêpnik u¿ywa siê soku z czarnej porzeczki, b¹dŸ te¿ syropu grenadine. Czêsto u¿ywa siê syropów zamiast likierów, przez co drink jest jeszcze s³odszy lecz w klasycznym wykonaniu drink powinien byæ na bazie likierów",
                    Glass = "Highball",
                    Ingredients = new List<string> {
                        "wódka - 40 ml ",
                        "likier kokosowy - 20 ml",
                        "likier brzoskwiniowy - 20 ml",
                        "pomarañcza - kawa³ek do dekoracji",
                        "sok ¿urawinowy (splash) - 40 ml",
                        "sok pomarañczowy - 40 ml",
                        "lód - kilka kostek"
                    }
                },
                new DrinkViewModel {
                    Name = "Black Russian ",
                    Description =
                        @"Przygotowanie Black Russian nie powinno przysporzyæ wiêkszych trudnoœci ani przy zakupie sk³adników, ani przy samym przyrz¹dzaniu drinka. Wystarczy wype³niæ tumbler na lodzie, który jednoczeœnie bêdzie s³u¿yæ jako naczynie do podania drinka, do po³owy kostkami lodu. Nastêpnie nale¿y wlaæ 1,5 miarki wódki i 0,75 miarki likieru kawowego. Potem wystarczy ju¿ tylko bardzo delikatnie zamieszaæ drinka. Black Russian widnieje na liœcie przepisów IBA.",
                    Glass = "Tumbler",
                    Ingredients = new List<string> {
                        "wódka - 100 ml ",
                        "kalhua  - 50 ml",
                        "lód - kilka kostek"
                    }
                },
                new DrinkViewModel {
                    Name = "Black Russian ",
                    Description =
                        @"Wódkê i likier oraz kostki lodu umieœciæ w szklanicy barmañskiej i zamieszaæ. Przelaæ do kieliszka koktajlowego. Na powierzchni po³o¿yæ lekko ubit¹ œmietanê. Napój pije siê te¿ z kostkami lodu. Wówczas podaje siê go w ma³ej szklance typu tumbler.",
                    Glass = "Tumbler",
                    Ingredients = new List<string> {
                        "wódka - 100 ml ",
                        "kalhua  - 50 ml",
                        "lód - kilka kostek",
                        "skondensowane mleczko - 30 ml"
                    }
                },
                new DrinkViewModel {
                    Name = "Black Russian ",
                    Description =
                        @"Wlej rum,niebieskie curacao,sok z ananasa i krem kokosowy do blendera razem ze szklank¹ pokruszonego lodu. Miksuj przez 15 sekund lub do momentu uzyskania pienistej masy. Wlej do najdziwniejszego kieliszka, jaki znajdziesz i udekoruj plasterkiem ananasa oraz wisienka koktajlow¹.",
                    Glass = "Tumbler",
                    Ingredients = new List<string> {
                        "bia³y rum - 30 ml ",
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