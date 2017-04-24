using System.Collections.Generic;

namespace DrinkUp.WebApi.ViewModels {
    public class DrinkViewModel {
        public string Name { get; set; }
        public string Description { get; set; }
        public IList<string> Ingredients { get; set; }
        public string Glass { get; set; }
    }
}
