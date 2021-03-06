﻿using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Enums;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.PersonController.Commands.UpdatePerson;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.RelationController.Commands.DeleteRelation
{
    public class DeleteRelationHandler : IRequestHandler<DeleteRelationCommand>
    {
        private readonly IApplicationDbContext _context;

        private readonly ILogger<DeleteRelationHandler> _logger;

        public DeleteRelationHandler(IApplicationDbContext context, ILogger<DeleteRelationHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteRelationCommand request, CancellationToken cancellationToken)
        {
            var person = await _context.Person.SingleOrDefaultAsync(x => x.Id == request.PersonId, cancellationToken);

            if (person == default(Person))
            {
                throw new ApplicationMessageException(ApplicationExceptionCode.PersonNotFound);
            }

            var relation = await _context.Relation.SingleOrDefaultAsync(x => x.Id == request.RelationId, cancellationToken);

            if (relation == default(Relation))
            {
                throw new ApplicationMessageException(ApplicationExceptionCode.RelationNotFound);
            }

            relation.Delete();

            _context.Relation.Remove(relation);

            await _context.SaveChangesAsync(cancellationToken);

            return await Task.FromResult(Unit.Value);
        }
    }
}
