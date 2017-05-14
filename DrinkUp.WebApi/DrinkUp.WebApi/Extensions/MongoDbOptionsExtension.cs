﻿using DrinkUp.WebApi.Model;
using MongoDB.Driver;

namespace DrinkUp.WebApi.Extensions {
    public static class MongoDbOptionsExtension
    {
        public static FindOneAndUpdateOptions<T> GetFindOneAndUpdateOptions<T>() where T : IEntity {
            return new FindOneAndUpdateOptions<T> {
                IsUpsert = true
            };
        }
    }
}
