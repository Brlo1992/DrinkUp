using DrinkUp.WebApi.Model;
using DrinkUp.WebApi.Model.Service;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace DrinkUp.WebApi.Extensions {
    public static class MongoDrinkCollectionExtension {
        public static ServiceResult<IEnumerable<Drink>> GetByCondition(this IMongoCollection<Drink> db, Drink drink) {
            var result = new ServiceResult<IEnumerable<Drink>>();
            try {
                result.Data = db.FindSync(GetByDrink(drink)).ToList();
            }
            catch (Exception ex) {
                result.AddError(ex.Message);
            }
            return result;
        }
        public static FilterDefinition<Drink> GetByDrink(Drink drink) => Builders<Drink>.Filter
            .Where(x => 
                x.Name.ToLower().Contains(drink.Name.ToLower()) ||
                x.Glass.ToLower().Contains(drink.Glass.ToLower()));
    }
}
