using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.PersonController.Commands.UpdatePersonImage
{
    public class UpdatePersonImageCommand : IRequest
    {
        public int Id { get; set; }

        public IFormFile ProfileImage { get; set; }
    }
}
