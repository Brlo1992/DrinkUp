using System.Linq;
using System.Threading.Tasks;
using DrinkUp.WebApi.Context;
using DrinkUp.WebApi.Model;
using DrinkUp.WebApi.Model.Service;
using DrinkUp.WebApi.ViewModels;

namespace DrinkUp.WebApi.Services {
    public interface IDrinkService {
        Task<ServiceResult> Add(DrinkViewModel drink);

        Task<ServiceResult> Remove(IdentityViewModel identity);

        ServiceResult<IQueryable<Drink>> GetAll();

        Task<ServiceResult<Drink>> GetSingle(NameViewModel viewModel);

        Task<ServiceResult> Update(DrinkViewModel viewModel);
    }

    public class DrinkService : IDrinkService {
        private readonly IMongoContext db;

        public DrinkService(IMongoContext db) {
            this.db = db;
        }

        public Task<ServiceResult> Add(DrinkViewModel viewModel) => db.Insert(GetFromViewModel(viewModel));

        public Task<ServiceResult> Remove(IdentityViewModel viewModel) => db.Remove(viewModel.Id);

        public ServiceResult<IQueryable<Drink>> GetAll() => db.GetAll();

        public Task<ServiceResult<Drink>> GetSingle(NameViewModel viewModel) => db.GetSingle(viewModel.Name);

        public Task<ServiceResult> Update(DrinkViewModel viewModel) {
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