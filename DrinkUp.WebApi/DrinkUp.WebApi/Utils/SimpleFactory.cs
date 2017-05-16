namespace DrinkUp.WebApi.Utils {
    public static class SimpleFactory<T> where T : new() {
        public static T Create() {
            return new T();
        }
    }
}
