using System.Collections.Generic;
using System.Linq;
using DrinkUp.WebApi.Context;
using DrinkUp.WebApi.ViewModels;

namespace DrinkUp.WebApi.Services {
    public interface ISearchService {
        IList<SearchResultViewModel> Search(SearchViewModel viewModel);
    }

    public class SearchService : ISearchService {
        private readonly IMongoContext db;

        public SearchService(IMongoContext db) {
            this.db = db;
        }

        public IList<SearchResultViewModel> Search(SearchViewModel viewModel = null) => GetAll();

        private List<SearchResultViewModel> GetAll() => db.GetAll()
            .Select(x => new SearchResultViewModel {
                Name = x.Name
            }).ToList();
    }
}
