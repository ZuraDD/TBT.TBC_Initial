namespace Infrastructure.WebApi.Models
{
    public class OkResponse
    {
        public StatusCodeEnum Status { get; set; } = StatusCodeEnum.Ok;

        public object Data { get; set; }
    }
}
