using DrinkUp.WebApi.Context;
using DrinkUp.WebApi.Model;
using DrinkUp.WebApi.Model.Service;
using DrinkUp.WebApi.Utils;
using DrinkUp.WebApi.ViewModels;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static DrinkUp.WebApi.Utils.Common;

namespace DrinkUp.WebApi.Services {
    public interface IDrinkService {
        Task<ServiceResult> Add(DrinkViewModel drink);

        Task<ServiceResult> Remove(IdentityViewModel identity);

        Task<ServiceResult<IEnumerable<DrinkViewModel>>> GetAll();

        Task<ServiceResult<DrinkViewModel>> GetSingle(IdentityViewModel viewModel);

        Task<ServiceResult> Update(DrinkViewModel viewModel);
    }

    public class DrinkService : IDrinkService {
        private readonly IMongoContext db;

        public DrinkService(IMongoContext db) => this.db = db;

        public async Task<ServiceResult> Add(DrinkViewModel viewModel) => await db.Insert(MapFromViewModel(viewModel));

        public async Task<ServiceResult> Remove(IdentityViewModel viewModel) => await db.Remove(viewModel.Id);

        public async Task<ServiceResult<IEnumerable<DrinkViewModel>>> GetAll() {
            var result = ResultFactory.CreateWithData<IEnumerable<DrinkViewModel>>();
            var dbresult = await db.GetAll();

            if (IsNotNull(dbresult.Data))
                result.Data = dbresult.Data.Select(MapFromModel);

            result.AddErrors(dbresult.Errors);
            result.Status = dbresult.Status;

            return result;
        }

        public async Task<ServiceResult<DrinkViewModel>> GetSingle(IdentityViewModel viewModel) {
            var result = ResultFactory.CreateWithData<DrinkViewModel>();
            var dbresult = await db.GetSingle(viewModel.Id);

            if (IsNotNull(dbresult.Data))
                result.Data = MapFromModel(dbresult.Data);

            result.AddErrors(dbresult.Errors);
            result.Status = dbresult.Status;

            return result;
        }

        public async Task<ServiceResult> Update(DrinkViewModel viewModel) {
            var updateDefiniton = GetUpdateDefinition(viewModel);
            var result = await db.Update(viewModel, updateDefiniton);
            return result;
        }

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

        private Drink MapFromViewModel(DrinkViewModel viewModel) => new Drink {
            Name = viewModel.Name,
            Description = viewModel.Description,
            Glass = viewModel.Glass,
            Ingredients = viewModel.Ingredients,
        };

        private DrinkViewModel MapFromModel(Drink model) => new DrinkViewModel {
            Name = model.Name,
            Description = model.Description,
            Id = model.Id.ToString(),
            Glass = model.Glass,
            Ingredients = model.Ingredients
        };
    }
}