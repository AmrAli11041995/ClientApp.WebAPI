using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.DTOs.Common
{
    public class ResponseModel
    {
        [JsonProperty("isSuccess")]
        public bool IsSuccess { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("exceptionMessage")]
        public string ExceptionMessage { get; set; }
        [JsonProperty("statusCode")]
        public int? StatusCode { get; set; }
        [JsonProperty("data")]
        public object? Data { get; set; }
    }
}
