namespace TaskManager.Shared
{
    public class Result
    {
        public bool IsSuccess { get; private set; }
        public string Error { get; private set; }

        public bool IsFailure => !IsSuccess;

        protected Result(bool isSuccess, string? error)
        {
            IsSuccess = isSuccess;
            Error = error;
        }

        public static Result Ok() => new Result(true, default);
        public static Result Fail(string error) => new Result(false, error);
    }

    public class Result<T> : Result
    {
        public T? Data { get; }

        protected Result(bool isSuccess, T? data, string? error)
            : base(isSuccess, error)
        {
            Data = data;
        }

        public static Result<T> Ok(T data) => new Result<T>(true, data, default);

        public static Result<T> Fail(string error) => new Result<T>(false, default, error);
    }
}
