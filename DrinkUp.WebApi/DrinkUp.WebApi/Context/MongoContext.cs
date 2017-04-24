using System.Collections.Generic;
using System.Linq;
using DrinkUp.WebApi.Model;
using MongoDB.Bson;
using MongoDB.Shared;
using MongoDB.Driver;

namespace DrinkUp.WebApi.Context {
    public interface IMongoContext {
        IQueryable<Drink> GetAll();
        void Insert(Drink drink);
    }

    public class MongoContext : IMongoContext
    {
        private readonly MongoDatabaseBase db;
        private readonly MongoClient client;

        public MongoContext() {
            client = new MongoClient("mongodb://localhost:27017");
            db = client.GetDatabase("DrinkUpDb") as MongoDatabaseBase;
        }

        public IQueryable<Drink> GetAll() {
            return GetDrinkCollection()
                .AsQueryable();
        }

        public void Insert(Drink drink) {
            GetDrinkCollection()
                .InsertOne(drink);
        }

        private IMongoCollection<Drink> GetDrinkCollection() {
            return db.GetCollection<Drink>("Drinks");
        }
    }
}