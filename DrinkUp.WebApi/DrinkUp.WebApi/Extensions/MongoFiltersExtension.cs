using MongoDB.Bson;
using MongoDB.Driver;

namespace DrinkUp.WebApi.Extensions {
    public static class MongoFiltersExtension {
        public static FilterDefinition<T> GetById<T>(string id) => Builders<T>.Filter.Eq("_id", ObjectId.Parse(id));

        public static FilterDefinition<T> GetByName<T>(string name) => Builders<T>.Filter.Eq("Name", name);

        public static FilterDefinition<T> EmptyFilter<T>() => Builders<T>.Filter.Empty;
    }
}