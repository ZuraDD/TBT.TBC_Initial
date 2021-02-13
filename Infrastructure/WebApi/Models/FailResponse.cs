using System.Collections.Generic;

namespace Infrastructure.WebApi.Models
{
    public class FailResponse
    {
        public StatusCodeEnum Status { get; set; } = StatusCodeEnum.Fail;

        public string ErrorMessage { get; set; }

        public IDictionary<string, string[]> ErrorDetails { get; set; }
    }
}
