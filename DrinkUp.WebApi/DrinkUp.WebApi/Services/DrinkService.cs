using DrinkUp.WebApi.Context;
using DrinkUp.WebApi.Model;
using DrinkUp.WebApi.Model.Service;
using DrinkUp.WebApi.ViewModels;
using MongoDB.Driver;
using System.Linq;
using System.Threading.Tasks;

namespace DrinkUp.WebApi.Services {
    public interface IDrinkService {
        Task<ServiceResult> Add(DrinkViewModel drink);

        Task<ServiceResult> Remove(IdentityViewModel identity);

        ServiceResult<IQueryable<Drink>> GetAll();

        Task<ServiceResult<Drink>> GetSingle(IdentityViewModel viewModel);

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

        public Task<ServiceResult<Drink>> GetSingle(IdentityViewModel viewModel) => db.GetSingle(viewModel.Id);

        public Task<ServiceResult> Update(DrinkViewModel viewModel) => db.Update(viewModel.Id.ToString(), GetUpdateDefinition(viewModel));

        private UpdateDefinition<Drink> GetUpdateDefinition(DrinkViewModel viewModel) {
            var builder = new UpdateDefinitionBuilder<Drink>();
            var name = new StringFieldDefinition<Drink, string>(nameof(viewModel.Name));
            var description = new StringFieldDefinition<Drink, string>(nameof(viewModel.Description));
            var glass = new StringFieldDefinition<Drink, string>(nameof(viewModel.Glass));

            return builder
                .Set(name, viewModel.Name)
                .Set(description, viewModel.Description)
                .Set(glass, viewModel.Glass);
        }

        private static Drink GetFromViewModel(DrinkViewModel viewModel) => new Drink {
            Name = viewModel.Name,
            Description = viewModel.Description,
            Glass = viewModel.Glass,
            Ingredients = viewModel.Ingredients,
        };
    }
}