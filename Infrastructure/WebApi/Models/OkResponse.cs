namespace Infrastructure.WebApi.Models
{
    public class OkResponse
    {
        public string Status { get; set; } = StatusCodeEnum.Ok.ToString();

        public object Data { get; set; }
    }
}
