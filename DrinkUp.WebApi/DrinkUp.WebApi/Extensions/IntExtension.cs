namespace DrinkUp.WebApi.Extensions {
    public enum IntValues {
        Zero = 0,
        One = 1
    }

    public static class IntExtension {
        public static bool IsGreaterThan(this int currentValue, int value = (int)IntValues.Zero) => 
            currentValue > value;

        public static bool IsEqualTo(this int currentValue, int value = (int)IntValues.Zero) => 
            currentValue == value;
    }
}
