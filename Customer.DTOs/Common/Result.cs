using Newtonsoft.Json;

namespace Customer.DTOs.Common
{
    public class Result<T>
    {
        public Result()
        {
            Status = false;
            Data = default(T);
        }

        public Result(T data)
        {
            StatusCode = 200;
            Status = true;
            Data = data;
        }

        public Result(T data, string message, string exceptionMessage, int statusCode, bool status = false) : this(data)
        {
            StatusCode = statusCode;
            Message = message;
            ExceptionMessage = exceptionMessage;
            Status = status;
        }

        public Result(string message, string exceptionMessage, int statusCode, bool status = false) : this()
        {
            StatusCode = statusCode;
            Message = message;
            ExceptionMessage = exceptionMessage;
            Status = status;
        }

        [JsonProperty(PropertyName = "statusCode")]
        public int StatusCode { get; set; }

        [JsonProperty(PropertyName = "status")]
        public bool Status { get; set; }
        [JsonProperty(PropertyName = "result")]
        public T Data { get; set; }
        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }
        [JsonProperty(PropertyName = "exceptionMessage")]
        public string ExceptionMessage { get; set; }
    }
}
