using DrinkUp.WebApi.Model;
using DrinkUp.WebApi.Model.Service;
using DrinkUp.WebApi.Utils;
using DrinkUp.WebApi.ViewModels;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static DrinkUp.WebApi.Extensions.MongoFiltersExtension;

namespace DrinkUp.WebApi.Extensions {
    public static class MongoDbExtension {
        public static async Task<ServiceResult<IEnumerable<T>>> GetMany<T>(this IMongoCollection<T> db) {
            var result = ResultFactory.CreateWithData<IEnumerable<T>>();
            try {
                result.Data = await db.Find(EmptyFilter<T>()).ToListAsync();
                result.Status = nameof(Status.ManySelected);
            }
            catch (Exception ex) {
                result.AddError(ex.Message);
                result.Status = nameof(Status.OperationFailed);
            }
            return result;
        }

        public static async Task<ServiceResult<T>> GetSingle<T>(this IMongoCollection<T> db, string id)
            where T : new() {
            var result = ResultFactory.CreateWithData<T>();
            try {
                var queryResult = await db.Find(GetById<T>(id)).ToListAsync();
                if (queryResult.IsOneSelected()) {
                    result.Data = queryResult.First();
                    result.Status = nameof(Status.OneSelected);
                }
                else if (queryResult.IsEmpty()) {
                    result.Status = nameof(Status.NotExist);
                }
                else {
                    result.Status = nameof(Status.FindMoreThanOne);
                }
            }
            catch (Exception ex) {
                result.AddError(ex.Message);
                result.Status = nameof(Status.OperationFailed);
            }
            return result;
        }

        public static async Task<ServiceResult> TryInsert<T>(this IMongoCollection<T> db, T item) where T : IEntity {
            var result = ResultFactory.Create();
            try {
                var queryResult = await db.Find(GetByName<T>(item.Name)).ToListAsync();
                if (queryResult.IsEmpty()) {
                    await db.InsertOneAsync(item);
                    result.Status = nameof(Status.Added);
                }
                else {
                    result.Status = nameof(Status.AlreadyExist);
                }
            }
            catch (Exception ex) {
                result.AddError(ex.Message);
                result.Status = nameof(Status.OperationFailed);
            }
            return result;
        }

        public static async Task<ServiceResult> TryDelete<T>(this IMongoCollection<T> db, string id) {
            var result = ResultFactory.Create();
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

        public static async Task<ServiceResult> TryUpdate<T>(this IMongoCollection<T> db, DrinkViewModel viewModel,
            UpdateDefinition<T> updateDefinition) where T : IEntity {
            var result = ResultFactory.Create();
            try {
                var checkByNameResult = await db.Find(GetByName<T>(viewModel.Name)).ToListAsync();

                if (checkByNameResult.IsEmpty()) {
                    var queryResult = await db.Find(GetById<T>(viewModel.Id)).ToListAsync();

                    if (queryResult.IsOneSelected()) {
                        await db.FindOneAndUpdateAsync(GetById<T>(viewModel.Id), updateDefinition);
                        result.Status = nameof(Status.Updated);
                    }
                    else {
                        result.Status = nameof(Status.FindMoreThanOne);
                    }
                }
                else {
                    result.Status = nameof(Status.NameAlreadyExist);
                }
            }
            catch (Exception ex) {
                result.AddError(ex.Message);
                result.Status = nameof(Status.OperationFailed);
            }
            return result;
        }
    }
}