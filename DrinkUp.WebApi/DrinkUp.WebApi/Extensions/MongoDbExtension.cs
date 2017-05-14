using DrinkUp.WebApi.Model;
using DrinkUp.WebApi.Model.Service;
using MongoDB.Driver;
using System;
using System.Linq;
using System.Threading.Tasks;
using static DrinkUp.WebApi.Extensions.MongoBuildersExtension;

namespace DrinkUp.WebApi.Extensions {
    public static class MongoDbExtension {
        public static ServiceResult<IQueryable<T>> GetMany<T>(this IMongoCollection<T> db) {
            var result = new ServiceResult<IQueryable<T>>();
            try {
                result.Data = db.AsQueryable();
            }
            catch (Exception ex) {
                result.AddError(ex.Message);
            }
            return result;
        }

        public static async Task<ServiceResult<T>> GetSingle<T>(this IMongoCollection<T> db, string id) where T : new() {
            var result = new ServiceResult<T>();
            try {
                var queryResult = await db.Find(GetById<T>(id)).ToListAsync();
                result.Data = queryResult.First();
            }
            catch (Exception ex) {
                result.AddError(ex.Message);
            }
            return result;
        }

        public static async Task<ServiceResult> TryInsert<T>(this IMongoCollection<T> db, T item) where T : IEntity {
            var result = new ServiceResult();
            try {
                await db.InsertOneAsync(item);
            }
            catch (Exception ex) {
                result.AddError(ex.Message);
            }
            return result;
        }

        public static async Task<ServiceResult> TryDelete<T>(this IMongoCollection<T> db, string id) {
            var result = new ServiceResult();
            try {
                await db.FindOneAndDeleteAsync(GetById<T>(id));
            }
            catch (Exception ex) {
                result.AddError(ex.Message);
            }
            return result;
        }

        public static async Task<ServiceResult> TryUpdate<T>(this IMongoCollection<T> db, T update)
            where T : IEntity {
            var result = new ServiceResult();
            //try {
            //    await db.FindOneAndUpdateAsync(GetById<T>(update.Id), GetUpdateDefinitions(update));
            //}
            //catch (Exception ex) {
            //    result.AddError(ex.Message);
            //}
            return result;
        }

    }
}
