using DrinkUp.WebApi.Context;
using DrinkUp.WebApi.ViewModels;
using System;
using System.Collections.Generic;

namespace DrinkUp.WebApi.Services {
    public interface ISearchService {
        IList<SearchResultViewModel> Search(SearchViewModel viewModel);
    }

    public class SearchService : ISearchService {
        private readonly IMongoContext db;

        public SearchService(IMongoContext db) {
            this.db = db;
        }

        public IList<SearchResultViewModel> Search(SearchViewModel viewModel = null) => throw new NotImplementedException();
    }
}
