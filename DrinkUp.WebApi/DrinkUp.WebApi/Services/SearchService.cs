using System.Collections.Generic;
using DrinkUp.WebApi.ViewModels;

namespace DrinkUp.WebApi.Services {
    public interface ISearchService {
        IList<SearchResultViewModel> Search(SearchViewModel viewModel);
    }

    public class SearchService : ISearchService {
        public IList<SearchResultViewModel> Search(SearchViewModel viewModel) {
            return new List<SearchResultViewModel>();
        }
    }
}
