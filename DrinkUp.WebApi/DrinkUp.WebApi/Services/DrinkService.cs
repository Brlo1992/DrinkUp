using System.Linq;
using DrinkUp.WebApi.Context;
using DrinkUp.WebApi.Model;
using DrinkUp.WebApi.Model.Service;
using DrinkUp.WebApi.ViewModels;

namespace DrinkUp.WebApi.Services {
    public interface IDrinkService {
        ServiceResult Add(DrinkViewModel drink);

        ServiceResult Remove(IdentityViewModel identity);

        ServiceResult<IQueryable<Drink>> GetAll();

        ServiceResult GetSingle(SearchViewModel viewModel);

        ServiceResult Update(DrinkViewModel viewModel);
    }

    public class DrinkService : IDrinkService {
        private readonly IMongoContext db;

        public DrinkService(IMongoContext db) {
            this.db = db;
        }

        public ServiceResult Add(DrinkViewModel viewModel) => db.Insert(GetFromViewModel(viewModel));

        public ServiceResult Remove(IdentityViewModel viewModel) => db.Remove(viewModel.Id);

        public ServiceResult<IQueryable<Drink>> GetAll() => db.GetAll();

        public ServiceResult GetSingle( IdentityViewModel viewModel) => db.GetSingle(viewModel.Id);

        public ServiceResult Update(DrinkViewModel viewModel) {
            throw new System.NotImplementedException();
        }

        private static Drink GetFromViewModel(DrinkViewModel viewModel) => new Drink {
            Name = viewModel.Name,
            Description = viewModel.Description,
            Glass = viewModel.Glass,
            Ingredients = viewModel.Ingredients,
        };
    }
}