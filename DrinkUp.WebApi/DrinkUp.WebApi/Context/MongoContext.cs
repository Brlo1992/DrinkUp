using DrinkUp.WebApi.Extensions;
using DrinkUp.WebApi.Model;
using DrinkUp.WebApi.Model.Service;
using MongoDB.Driver;
using System.Linq;
using static DrinkUp.WebApi.Extensions.MongoUpdateDefinitionExtension;

namespace DrinkUp.WebApi.Context {
    public interface IMongoContext {
        ServiceResult<IQueryable<Drink>> GetAll();

        ServiceResult<Drink> GetSingle(string id);

        ServiceResult Insert(Drink drink);

        ServiceResult Remove(int id);

        ServiceResult Update(Drink drink);
    }

    public class MongoContext : IMongoContext {
        private readonly MongoDatabaseBase db;
        private readonly MongoClient client;

        public MongoContext() {
            client = new MongoClient("mongodb://localhost:27017");
            db = client.GetDatabase("DrinkUpDb") as MongoDatabaseBase;
        }

        public ServiceResult<IQueryable<Drink>> GetAll() => Drinks.GetMany();

        public ServiceResult<Drink> GetSingle(string name) => Drinks.GetSingle(name);

        public ServiceResult Insert(Drink drink) => Drinks.TryInsert(drink);

        public ServiceResult Update(Drink drink) => Drinks.TryUpdate(drink);

        public ServiceResult Remove(int id) => Drinks.TryDelete(id);

        private IMongoCollection<Drink> Drinks => db.GetCollection<Drink>("Drinks");
    }
}