using MediatR;

namespace Application.PersonController.Commands.DeletePerson
{
    public class DeletePersonCommand : IRequest
    {
        public int Id { get; set; }
    }
}
