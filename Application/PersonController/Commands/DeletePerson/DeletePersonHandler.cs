using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Enums;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.PersonController.Commands.DeletePerson
{
    public class DeletePersonHandler : IRequestHandler<DeletePersonCommand>
    {
        private readonly IApplicationDbContext _context;

        private readonly ILogger<DeletePersonHandler> _logger;

        public DeletePersonHandler(IApplicationDbContext context, ILogger<DeletePersonHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeletePersonCommand request, CancellationToken cancellationToken)
        {
            var person = _context.Person.SingleOrDefault(x => x.Id == request.Id);

            if (person == default(Person))
            {
                throw new ApplicationMessageException(ApplicationExceptionCode.PersonNotFound);
            }

            person.Delete();

            _context.Person.Remove(person);

            await _context.SaveChangesAsync(cancellationToken);

            return await Task.FromResult(Unit.Value);
        }
    }
}
