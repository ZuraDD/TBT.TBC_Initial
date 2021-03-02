using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Application.PersonController.Commands.DeletePerson
{
    public class DeletePersonCommand : IRequest
    {
        [FromRoute(Name = "personId")]
        public int Id { get; set; }
    }
}
