namespace MMAppApi.Helpers
{
    public class Result<T>
    {
        public bool IsSuccess { get; }
        public T? Value { get; }
        public string? ErrorMessage { get; }
        public Result(bool success, T? value, string? errorMessage)
        {
            IsSuccess = success;
            Value = value;
            ErrorMessage = errorMessage;
        }

        public static Result<T> Success(T value) => new Result<T>(true, value, null);
        public static Result<T> Failure(string errorMessage) => new Result<T>(false, default(T), errorMessage);
    }
}
