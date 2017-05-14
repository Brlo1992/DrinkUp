using DrinkUp.WebApi.Extensions;
using DrinkUp.WebApi.Model;
using DrinkUp.WebApi.Model.Service;
using MongoDB.Driver;
using System.Linq;
using System.Threading.Tasks;

namespace DrinkUp.WebApi.Context {
    public interface IMongoContext {
        ServiceResult<IQueryable<Drink>> GetAll();

        ServiceResult<IQueryable<Drink>> GetByCondition(Drink drink);

        Task<ServiceResult<Drink>> GetSingle(string id);

        Task<ServiceResult> Insert(Drink drink);

        Task<ServiceResult> Remove(string id);

        Task<ServiceResult> Update(Drink drink);
    }

    public class MongoContext : IMongoContext {
        private readonly MongoDatabaseBase db;

        public MongoContext() {
            var client = new MongoClient("mongodb://localhost:27017");
            db = client.GetDatabase("DrinkUpDb") as MongoDatabaseBase;
        }

        public ServiceResult<IQueryable<Drink>> GetAll() => Drinks.GetMany();

        public ServiceResult<IQueryable<Drink>> GetByCondition(Drink drink) => Drinks.GetByCondition(drink);

        public async Task<ServiceResult<Drink>> GetSingle(string id) => await Drinks.GetSingle(id);
               
        public async Task<ServiceResult> Insert(Drink drink) => await Drinks.TryInsert(drink);
               
        public async Task<ServiceResult> Update(Drink drink) => await Drinks.TryUpdate(drink);
               
        public async Task<ServiceResult> Remove(string id) => await Drinks.TryDelete(id);

        private IMongoCollection<Drink> Drinks => db.GetCollection<Drink>("Drinks");
    }
}