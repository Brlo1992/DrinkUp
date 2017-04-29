using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DrinkUp.WebApi.Extensions {
    public static class MongoUpdateDefinitionExtension {

        public static UpdateDefinition<T> GetUpdateDefinitions<T>(T update) {
            return Builders<T>.Update
                .Set("name", "name")
                .Set("","");
        }

    }
}
