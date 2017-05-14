using System.Collections.Generic;
using MongoDB.Bson;

namespace DrinkUp.WebApi.Model {
    public class Drink : IEntity{
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public IList<string> Ingredients { get; set; }
        public string Glass { get; set; }
        public int IsGood { get; set; }
        public int IsBad { get; set; }
        public int TopCount{ get; set; }
        public string Description { get; set; }
        public IList<string> Comments { get; set; }
        public IList<string> Images { get; set; }
    }
}