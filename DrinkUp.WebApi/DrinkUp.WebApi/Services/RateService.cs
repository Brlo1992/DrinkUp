using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DrinkUp.WebApi.Model.Service;
using DrinkUp.WebApi.Utils;
using DrinkUp.WebApi.ViewModels;

namespace DrinkUp.WebApi.Services {
    public enum RateType {
        Like,
        Unlike
    }

    public interface IRateService {
        Task<ServiceResult> RateDrink(IdentityViewModel viewModel, RateType rateType);
    }

    public class RateService : IRateService {
        private readonly IDrinkService drinkService;

        public RateService(IDrinkService drinkService) {
            this.drinkService = drinkService;
        }

        public async Task<ServiceResult> RateDrink(IdentityViewModel viewModel, RateType rateType) {
            var result = await drinkService.GetSingle(viewModel);

            if (result.IsValid) {
                if (rateType == RateType.Like)
                    result.Data.Like++;
                if (rateType == RateType.Unlike)
                    result.Data.Unlike++;

                return await drinkService.Update(result.Data);
            }

            return result;
        }
    }
}