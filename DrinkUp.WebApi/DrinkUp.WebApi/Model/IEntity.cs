using MongoDB.Bson;

namespace DrinkUp.WebApi.Model {
    public interface IEntity {
        ObjectId Id { get; set; }
        string Name { get; set; }
    }
}
