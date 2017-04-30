using DrinkUp.WebApi.Context;
using DrinkUp.WebApi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using DrinkUp.WebApi.Model.Service;

namespace DrinkUp.WebApi.Services {
    public interface ISearchService {
        ServiceResult<IQueryable<SearchResultViewModel>> Search(SearchViewModel viewModel);
    }

    public class SearchService : ISearchService {
        private readonly IMongoContext db;

        public SearchService(IMongoContext db) {
            this.db = db;
        }

        public ServiceResult<IQueryable<SearchResultViewModel>> Search(SearchViewModel viewModel = null) =>
            db.GetByCondiotion(viewModel);
    }
}
