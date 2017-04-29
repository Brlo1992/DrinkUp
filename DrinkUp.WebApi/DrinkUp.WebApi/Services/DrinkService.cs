using DrinkUp.WebApi.Context;
using DrinkUp.WebApi.Model;
using DrinkUp.WebApi.Model.Service;
using DrinkUp.WebApi.ViewModels;

namespace DrinkUp.WebApi.Services
{
    public interface IDrinkService
    {
        ServiceResult Add(DrinkViewModel drink);

        ServiceResult Remove(IdentityViewModel identity);
    }

    public class DrinkService : IDrinkService
    {
        private readonly IMongoContext db;

        public DrinkService(IMongoContext db) {
            this.db = db;
        }

        public ServiceResult Add(DrinkViewModel drink)
        {
            var result = new ServiceResult();
            var newDrink = GetFromViewModel(drink);
            db.Insert(newDrink);
            return result;  
        }

        public ServiceResult Remove(IdentityViewModel identity) {
            var result = new ServiceResult();
            db.Remove(identity.Id);
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