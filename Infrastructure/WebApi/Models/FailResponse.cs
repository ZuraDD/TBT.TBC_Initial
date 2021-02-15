using System.Collections.Generic;

namespace Infrastructure.WebApi.Models
{
    public class FailResponse
    {
        public string Status { get; set; } = StatusCodeEnum.Fail.ToString();

        public string ErrorMessage { get; set; }

        public IDictionary<string, string[]> ErrorDetails { get; set; }
    }
}
