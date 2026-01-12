namespace MMAppApi.Helpers
{
    public static class GenericHelper
    {
        public static T? ConvertValue<T>(object value) where T : class 
        {
            if (value == null)
            {
                // Return the default value for the type (e.g., null for reference types, 0 for int)
                return default(T);
            }

            // Use Convert.ChangeType to perform the conversion
            // This handles a wide range of built-in type conversions (e.g., int to float, string to int)
            try
            {
                return (T)Convert.ChangeType(value, typeof(T));
            }
            catch (InvalidCastException)
            {
                // Handle cases where the conversion is not supported
                Console.WriteLine($"Cannot convert type {value.GetType().Name} to {typeof(T).Name}");
                throw;
            }
            catch (FormatException)
            {
                // Handle cases where the input string is not in the correct format
                Console.WriteLine($"Input string is not in the correct format for type {typeof(T).Name}");
                throw;
            }
        }
    }
}
