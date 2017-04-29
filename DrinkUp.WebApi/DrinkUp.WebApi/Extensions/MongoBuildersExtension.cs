using MongoDB.Driver;

namespace DrinkUp.WebApi.Extensions {
    public static class MongoBuildersExtension {

        public static FilterDefinition<T> GetById<T>(int id) => Builders<T>.Filter.Eq("id", id);

        public static FilterDefinition<T> GetByName<T>(string name) => Builders<T>.Filter.Eq("name", name);
    }
}
