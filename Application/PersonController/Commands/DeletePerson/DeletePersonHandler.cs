using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Enums;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
            var person = await _context.Person.SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (person == default(Person))
            {
                throw new ApplicationMessageException(ApplicationExceptionCode.PersonNotFound);
            }

            var relations = await _context.Relation.Where(x => x.PersonFor.Id == person.Id || x.PersonToId == person.Id).ToListAsync(cancellationToken);

            foreach (var relation in relations)
            {
                relation.Delete();

                _context.Relation.Remove(relation);
            }

            person.Delete();

            _context.Person.Remove(person);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
