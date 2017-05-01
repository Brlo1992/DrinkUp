using DrinkUp.WebApi.Model;
using DrinkUp.WebApi.Model.Service;
using MongoDB.Driver;
using System;
using System.Linq;


namespace DrinkUp.WebApi.Extensions {
    public static class MongoDrinkCollectionExtension {
        public static ServiceResult<IQueryable<Drink>> GetByCondition(this IMongoCollection<Drink> db, Drink drink) {
            var result = new ServiceResult<IQueryable<Drink>>();
            try {
                result.Data = db.FindSync(GetByDrink(drink)).ToList().AsQueryable();
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
