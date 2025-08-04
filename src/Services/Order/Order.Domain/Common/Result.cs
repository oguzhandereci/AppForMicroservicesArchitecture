using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Domain.Common
{
    public sealed class Result<T> where T : class
    {
        public bool IsSuccess { get; }
        public bool IsFailure { get; }
        public Error? Error { get; }
        public T? Value { get; }

        private Result(bool isSuccess, Error? error, T? value)
        {
            IsSuccess = isSuccess;
            IsFailure = !isSuccess;
            Error = error;
            Value = value;
        }

        public static Result<T> Success(T value) => new Result<T>(true, null, value);

        public static Result<T> Failure(Error error) => new Result<T>(false, error, null);
    }
}
