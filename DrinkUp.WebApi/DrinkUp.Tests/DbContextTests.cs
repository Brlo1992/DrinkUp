using System.Linq;
using DrinkUp.WebApi.Context;
using Xunit;

namespace DrinkUp.Tests.Unit {
    public class DbContextTests
    {
        [Fact]
        public void should_get_all_rows_from_database() {
            
            var context = new MongoContext();

            var result = context.GetAll();

            Assert.Empty(result.Errors);
            Assert.Equal(3, result.Data.Count());
        }
    }
}
