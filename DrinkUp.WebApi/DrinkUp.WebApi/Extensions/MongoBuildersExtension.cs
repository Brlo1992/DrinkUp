using MongoDB.Bson;
using MongoDB.Driver;

namespace DrinkUp.WebApi.Extensions {
    public static class MongoBuildersExtension {

        public static FilterDefinition<T> GetById<T>(string id) => Builders<T>.Filter.Eq("_id", ObjectId.Parse(id));

        public static FilterDefinition<T> GetByName<T>(string name) => Builders<T>.Filter.Eq("name", name);
    }
}
