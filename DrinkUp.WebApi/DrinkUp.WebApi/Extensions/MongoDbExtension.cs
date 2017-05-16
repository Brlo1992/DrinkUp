using DrinkUp.WebApi.Model;
using DrinkUp.WebApi.Model.Service;
using MongoDB.Driver;
using System;
using System.Linq;
using System.Threading.Tasks;
using DrinkUp.WebApi.Utils;
using static DrinkUp.WebApi.Extensions.MongoBuildersExtension;

namespace DrinkUp.WebApi.Extensions {
    public static class MongoDbExtension {
        public static ServiceResult<IQueryable<T>> GetMany<T>(this IMongoCollection<T> db) {
            var result = SimpleFactory<ServiceResult<IQueryable<T>>>.Create();
            try {
                result.Data = db.AsQueryable();
                result.Status = nameof(Status.ManySelected);
            }
            catch (Exception ex) {
                result.AddError(ex.Message);
                result.Status = nameof(Status.OperationFailed);
            }
            return result;
        }

        public static async Task<ServiceResult<T>> GetSingle<T>(this IMongoCollection<T> db, string id) where T : new() {
            var result = SimpleFactory<ServiceResult<T>>.Create();
            try {
                var queryResult = await db.Find(GetById<T>(id)).ToListAsync();
                result.Data = queryResult.First();
                result.Status = nameof(Status.OneSelected);
            }
            catch (Exception ex) {
                result.AddError(ex.Message);
                result.Status = nameof(Status.OperationFailed);
            }
            return result;
        }

        public static async Task<ServiceResult> TryInsert<T>(this IMongoCollection<T> db, T item) where T : IEntity {
            var result = SimpleFactory<ServiceResult<T>>.Create();
            try {
                var queryResult = await db.Find(GetByName<T>(item.Name)).ToListAsync();
                if (queryResult.Any()) {
                    result.Status = nameof(Status.AlreadyExist);
                }
                else {
                    await db.InsertOneAsync(item);
                    result.Status = nameof(Status.Added);
                }

            }
            catch (Exception ex) {
                result.AddError(ex.Message);
                result.Status = nameof(Status.OperationFailed);
            }
            return result;
        }

        public static async Task<ServiceResult> TryDelete<T>(this IMongoCollection<T> db, string id) {
            var result = SimpleFactory<ServiceResult<T>>.Create();
            try {
                await db.FindOneAndDeleteAsync(GetById<T>(id));
                result.Status = nameof(Status.Removed);
            }
            catch (Exception ex) {
                result.AddError(ex.Message);
                result.Status = nameof(Status.OperationFailed);
            }
            return result;
        }

        public static async Task<ServiceResult> TryUpdate<T>(this IMongoCollection<T> db,
            string id,
            UpdateDefinition<T> updateDefinition)
            where T : IEntity {
            var result = SimpleFactory<ServiceResult<T>>.Create();
            try {
                await db.FindOneAndUpdateAsync(GetById<T>(id), updateDefinition);
            }
            catch (Exception ex) {
                result.AddError(ex.Message);
                result.Status = nameof(Status.OperationFailed);
            }
            return result;
        }

    }
}
