using BulkCertificationTool.Contracts;

namespace BulkCertificationTool.Models
{
    internal class Result<T> : IResult<T>
    {
        public Result(T result, string error)
        {
            _result = result;
            _error = error;
        }

        public Result(T result)
        {
            _result = result;
        }

        public bool HasError { get { return !string.IsNullOrWhiteSpace(_error); } }
        public string Error { get { return _error; } }
        public T Value { get { return _result; } }

        private readonly T _result;
        private readonly string _error;
    }
}
