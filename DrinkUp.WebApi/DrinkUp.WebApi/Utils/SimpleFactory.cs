using DrinkUp.WebApi.Model.Service;

namespace DrinkUp.WebApi.Utils {
    public static class SimpleFactory<T> where T : new() {
        public static T Create() => new T();
    }
}
