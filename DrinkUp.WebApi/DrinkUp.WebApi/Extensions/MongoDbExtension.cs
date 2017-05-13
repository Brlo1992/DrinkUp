using DrinkUp.WebApi.Model;
using DrinkUp.WebApi.Model.Service;
using MongoDB.Driver;
using System;
using System.Linq;
using System.Threading.Tasks;
using static DrinkUp.WebApi.Extensions.MongoBuildersExtension;
using static DrinkUp.WebApi.Extensions.MongoUpdateDefinitionExtension;

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

        public static async Task<ServiceResult<T>> GetSingle<T>(this IMongoCollection<T> db, string name) where T : new() {
            var result = new ServiceResult<T>();
            try {
                var queryResult = await db.FindAsync(GetByName<T>(name)).ToAsyncEnumerable().First();
                result.Data = queryResult.Current.Any() ? 
                    queryResult.Current.First() : 
                    new T();
            }
            catch (Exception ex) {
                result.AddError(ex.Message);
            }
            return result;
        }

        public static async Task<ServiceResult> TryInsert<T>(this IMongoCollection<T> db, T item) where T : IEntity {
            var result = new ServiceResult();
            try {
                await db.FindOneAndUpdateAsync(GetByName<T>(item.Name),
                    GetUpdateDefinitions(item),
                    GetFindOneAndUpdateOptions<T>());
            }
            catch (Exception ex) {
                result.AddError(ex.Message);
            }
            return result;
        }

        private static FindOneAndUpdateOptions<T> GetFindOneAndUpdateOptions<T>() where T : IEntity {
            return new FindOneAndUpdateOptions<T> {
                IsUpsert = true
            };
        }

        public static async Task<ServiceResult> TryDelete<T>(this IMongoCollection<T> db, int id) {
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
            try {
                await db.FindOneAndUpdateAsync(GetById<T>(update.Id), GetUpdateDefinitions(update));
            }
            catch (Exception ex) {
                result.AddError(ex.Message);
            }
            return result;
        }

    }
}
