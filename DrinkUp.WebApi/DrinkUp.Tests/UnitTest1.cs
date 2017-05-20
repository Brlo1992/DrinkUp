using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DrinkUp.WebApi.Context;
using DrinkUp.WebApi.Model;
using DrinkUp.WebApi.Model.Service;
using NSubstitute;
using Xunit;

namespace DrinkUp.Tests {
    public class UnitTest1
    {
        [Fact]
        public async Task should_get_all_rows_from_database() {
            var db = GetDbSubstitutes();

            var result = await db.GetAll();

            Assert.NotNull(result);
            Assert.NotNull(result.Data);
            Assert.Empty(result.Errors);
            Assert.Equal(4, result.Data.Count());
        }

        [Fact]
        public async Task should_get_single_row_from_database_by_name() {
            var db = GetDbSubstitutes();

            var result = await db.GetSingle("testowy1");

            Assert.NotNull(result);
            Assert.Empty(result.Errors);
            Assert.NotNull(result.Data);
        }

        private IMongoContext GetDbSubstitutes() {
            var db = Substitute.For<IMongoContext>();
            db.GetAll().Returns(GetSampleDrinks());
            return db;
        }

        private ServiceResult<IEnumerable<Drink>> GetSampleDrinks() {
            var result = new ServiceResult<IEnumerable<Drink>>() {
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
