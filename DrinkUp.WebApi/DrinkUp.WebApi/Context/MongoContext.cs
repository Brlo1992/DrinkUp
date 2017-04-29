using DrinkUp.WebApi.Model;
using MongoDB.Driver;
using System;
using System.Linq;
using DrinkUp.WebApi.Extensions;
using DrinkUp.WebApi.Model.Service;

namespace DrinkUp.WebApi.Context {
    public interface IMongoContext {
        ServiceResult<IQueryable<Drink>> GetAll();

        ServiceResult<Drink> GetSingle(int id);

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

        public ServiceResult<IQueryable<Drink>> GetAll() => GetDrinkCollection().GetMany();

        public ServiceResult<Drink> GetSingle(string name) => GetDrinkCollection().GetSingle(name);

        public ServiceResult Insert(Drink drink) => GetDrinkCollection().TryInsert(drink);

        public ServiceResult Update(Drink drink) => throw new NotImplementedException();

        public ServiceResult Remove(int id) => GetDrinkCollection().TryDelete(id);

        private IMongoCollection<Drink> GetDrinkCollection() => db.GetCollection<Drink>("Drinks");
    }
}