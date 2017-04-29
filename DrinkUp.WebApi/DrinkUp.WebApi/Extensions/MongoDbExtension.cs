﻿using DrinkUp.WebApi.Model.Service;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver.Linq;
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

        public static ServiceResult<T> GetSingle<T>(this IMongoCollection<T> db, string name) {
            var result = new ServiceResult<T>();
            try {
                result.Data = db.FindSync(GetByName<T>(name)).First();
            }
            catch (Exception ex) {
                result.AddError(ex.Message);
            }
            return result;
        }

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

        public static ServiceResult TryDelete<T>(this IMongoCollection<T> db, int id) {
            var result = new ServiceResult();
            try {
                db.DeleteOne(GetById<T>(id));
            }
            catch (Exception ex) {
                result.AddError(ex.Message);
            }
            return result;
        }

        public static ServiceResult TryUpdate<T>(this IMongoCollection<T> db, T update, int id) {
            var result = new ServiceResult();
            //try {
            //    db.UpdateOne(GetById<T>(id),GetUpdateDefinitions(update));
            //}
            //catch (Exception ex) {
            //    result.AddError(ex.Message);
            //}
            return result;
        }

    }
}