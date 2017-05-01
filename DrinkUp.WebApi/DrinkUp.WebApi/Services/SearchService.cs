using DrinkUp.WebApi.Context;
using DrinkUp.WebApi.Model.Service;
using DrinkUp.WebApi.ViewModels;
using System.Linq;
using DrinkUp.WebApi.Model;

namespace DrinkUp.WebApi.Services {
    public interface ISearchService {
        ServiceResult<IQueryable<SearchResultViewModel>> Search(SearchViewModel viewModel);
    }

    public class SearchService : ISearchService {
        private readonly IMongoContext db;

        public SearchService(IMongoContext db) {
            this.db = db;
        }

        public ServiceResult<IQueryable<SearchResultViewModel>> Search(SearchViewModel viewModel = null) {
            var model = GetFromViewModel(viewModel);
            var result = db.GetByCondition(model);
            var formatedResult = GetFormatedResult(result);
            return formatedResult;
        }

        private ServiceResult<IQueryable<SearchResultViewModel>> GetFormatedResult(ServiceResult<IQueryable<Drink>> result) {
            var formatedResult = new ServiceResult<IQueryable<SearchResultViewModel>> {
                Data = result.Data.Select(x => new SearchResultViewModel {
                    Name = x.Name,
                    Id = x.Id
                })
            };
            formatedResult.AddErrors(result.Errors);
            return formatedResult;
        }

        private Drink GetFromViewModel(SearchViewModel viewModel) {
            return new Drink {
                Name = viewModel.SearchPhase
            };
        }
    }
}
