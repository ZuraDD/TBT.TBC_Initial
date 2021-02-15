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

            if (await _context.Person.SingleOrDefaultAsync(x => x.Id == relation.PersonForId, cancellationToken) == default(Person) ||
                await _context.Person.SingleOrDefaultAsync(x => x.Id == relation.PersonToId, cancellationToken) == default(Person))
            {
                throw new ApplicationMessageException(ApplicationExceptionCode.PersonNotFound);
            }

            if (await _context.Relation.AnyAsync(x => x.PersonForId == relation.PersonForId && x.PersonToId == relation.PersonToId, cancellationToken))
            {
                throw new ApplicationMessageException(ApplicationExceptionCode.RelationAlreadyExists);
            }

            await _context.Relation.AddAsync(relation, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            return await Task.FromResult(Unit.Value);
        }
    }
}
