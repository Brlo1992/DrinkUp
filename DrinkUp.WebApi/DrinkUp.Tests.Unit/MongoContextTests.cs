using DrinkUp.WebApi.Context;
using DrinkUp.WebApi.Model;
using DrinkUp.WebApi.Model.Service;
using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace DrinkUp.Tests.Unit {
    [TestFixture]
    public class MongoContextTests {
        [Test]
        public void should_get_all_rows_from_database_correctly() {
            var db = GetDbSubstitutes();

            var result = db.GetAll();

            Assert.AreEqual(4, result.Data.Count());
        }

        private IMongoContext GetDbSubstitutes() {
            var db = Substitute.For<IMongoContext>();
            db.GetAll().Returns(GetSampleDrinks());
            return db;
        }

        private ServiceResult<IQueryable<Drink>> GetSampleDrinks() {
            var result = new ServiceResult<IQueryable<Drink>>() {
                Data = new List<Drink> {
                    new Drink { Name = "testowy1"},
                    new Drink { Name = "testowy2"},
                    new Drink { Name = "testowy3"},
                    new Drink { Name = "testowy4"}
                }.AsQueryable()
            };
            return result;
        }
    }
}
