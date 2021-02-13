using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Enums;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.RelationController.Commands.CreateRelation
{
    public class CreateRelationHandler : IRequestHandler<CreateRelationCommand>
    {
        private readonly IApplicationDbContext _context;

        private readonly ILogger<CreateRelationHandler> _logger;

        public CreateRelationHandler(IApplicationDbContext context, ILogger<CreateRelationHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Unit> Handle(CreateRelationCommand request, CancellationToken cancellationToken)
        {
            var relation = Relation.Create(request.RelationType, request.PersonFor, request.PersonTo);

            if (_context.Person.SingleOrDefault(x => x.Id == relation.PersonForId) == default(Person) ||
                _context.Person.SingleOrDefault(x => x.Id == relation.PersonToId) == default(Person))
            {
                throw new ApplicationMessageException(ApplicationExceptionCode.PersonNotFound);
            }

            if (_context.Relation.Any(x => x.PersonForId == relation.PersonForId && x.PersonToId == relation.PersonToId))
            {
                throw new ApplicationMessageException(ApplicationExceptionCode.RelationAlreadyExists);
            }

            await _context.Relation.AddAsync(relation, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            return await Task.FromResult(Unit.Value);
        }
    }
}
