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

        Task<ServiceResult> Update(string id, UpdateDefinition<Drink> updateDefinition);
    }

    public class MongoContext : IMongoContext {
        private readonly MongoDatabaseBase db;

        public MongoContext(string mongoConnection, string mongoCollection) {
            var client = new MongoClient(mongoConnection);
            db = client.GetDatabase(mongoCollection) as MongoDatabaseBase;
        }

        public ServiceResult<IQueryable<Drink>> GetAll() => Drinks.GetMany();

        public ServiceResult<IQueryable<Drink>> GetByCondition(Drink drink) => Drinks.GetByCondition(drink);

        public async Task<ServiceResult<Drink>> GetSingle(string id) => await Drinks.GetSingle(id);
               
        public async Task<ServiceResult> Insert(Drink drink) => await Drinks.TryInsert(drink);
               
        public async Task<ServiceResult> Update(string id, UpdateDefinition<Drink> updateDefinition) => await Drinks.TryUpdate(id, updateDefinition);
               
        public async Task<ServiceResult> Remove(string id) => await Drinks.TryDelete(id);

        private IMongoCollection<Drink> Drinks => db.GetCollection<Drink>("Drinks");
    }
}