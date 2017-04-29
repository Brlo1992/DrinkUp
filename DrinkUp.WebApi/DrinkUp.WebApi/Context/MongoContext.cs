using DrinkUp.WebApi.Model;
using MongoDB.Driver;
using System;
using System.Linq;
using DrinkUp.WebApi.Extensions;
using DrinkUp.WebApi.Model.Service;

namespace DrinkUp.WebApi.Context {
    public interface IMongoContext {
        IQueryable<Drink> GetAll();

        Drink GetSingle(int id);

        ServiceResult Insert(Drink drink);

        ServiceResult Delete(int id);

        ServiceResult Update(Drink drink);
    }

    public class MongoContext : IMongoContext {
        private readonly MongoDatabaseBase db;
        private readonly MongoClient client;

        public MongoContext() {
            client = new MongoClient("mongodb://localhost:27017");
            db = client.GetDatabase("DrinkUpDb") as MongoDatabaseBase;
        }

        public IQueryable<Drink> GetAll() => GetDrinkCollection().AsQueryable();

        public Drink GetSingle(int id) {
            throw new NotImplementedException();
        }

        public ServiceResult Insert(Drink drink) {
            return GetDrinkCollection().TryInsert(drink);
        }

        public ServiceResult Delete(int id) {
            throw new NotImplementedException();
        }

        public ServiceResult Update(Drink drink) {
            throw new NotImplementedException();
        }

        public ServiceResult Remove(int id) {
            throw new NotImplementedException();
        }

        private IMongoCollection<Drink> GetDrinkCollection() {
            return db.GetCollection<Drink>("Drinks");
        }
    }
}