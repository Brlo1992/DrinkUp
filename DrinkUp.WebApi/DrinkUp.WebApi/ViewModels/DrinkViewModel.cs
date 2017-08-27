using System.Collections.Generic;

namespace DrinkUp.WebApi.ViewModels {
    public class DrinkViewModel {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IList<string> Ingredients { get; set; }
        public string Glass { get; set; }
        public int Like { get; set; }
        public int Unlike { get; set; }
    }
}
