using DrinkUp.WebApi.Model.Service;
using MongoDB.Driver;
using System;

namespace DrinkUp.WebApi.Extensions {
    public static class MongoDbExtension {
        public static ServiceResult TryInsert<T>(this IMongoCollection<T> db, T item) {
            var result = new ServiceResult();
            try {
                db.InsertOne(item);
            }
            catch (Exception ex) {
                result.AddError(ex.Message);
            }
            return result;
        }

        public static ServiceResult TryDelete<T>(this IMongoCollection<T> db, FilterDefinition<T> filter) {
            var result = new ServiceResult();
            try {
                db.DeleteOne(filter);
            }
            catch (Exception ex) {
                result.AddError(ex.Message);
            }
            return result;
        }

        public static ServiceResult TryUpdate<T>(this IMongoCollection<T> db, UpdateDefinition<T> updateDefinition, FilterDefinition<T> filter) {
            var result = new ServiceResult();
            try {
                db.UpdateOne(filter, updateDefinition);
            }
            catch (Exception ex) {
                result.AddError(ex.Message);
            }
            return result;
        }
    }
}
