using Microsoft.Extensions.Configuration;

namespace DrinkUp.WebApi.Utils {
    public static class ConfigrationProvider {
        public static string GetMongoConnection(IConfigurationRoot config) =>
            $"{config["mongo:mongoConnection"]}";
        public static string GetMongoCollection(IConfigurationRoot config) => 
            $"{config["mongo:mongoCollection"]}";
    }
}
