using DrinkUp.WebApi.Context;
using DrinkUp.WebApi.Model;
using DrinkUp.WebApi.ViewModels;

namespace DrinkUp.WebApi.Services
{
    public interface IDrinkService
    {
        void Add(DrinkViewModel drink);
    }

    public class DrinkService : IDrinkService
    {
        private readonly IMongoContext db;

        public DrinkService(IMongoContext db) {
            this.db = db;
        }

        public void Add(DrinkViewModel drink)
        {
            var newDrink = GetFromViewModel(drink);
            db.Insert(newDrink);
        }

        private Drink GetFromViewModel(DrinkViewModel viewModel)
        {
            return new Drink
            {
                Name = viewModel.Name,
                Description = viewModel.Description,
                Glass = viewModel.Glass,
                Ingredients = viewModel.Ingredients,
            };
        }
    }
}