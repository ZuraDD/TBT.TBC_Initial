using Application.PersonController.Queries.GetPersonInfo.Models;
using MediatR;

namespace Application.PersonController.Queries.GetPersonInfo
{
    public class GetPersonInfoQuery : IRequest<GetPersonInfoDto>
    {
        public int PersonId { get; set; }
    }
}
