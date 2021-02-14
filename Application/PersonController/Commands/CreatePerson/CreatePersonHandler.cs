using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Enums;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.PersonController.Commands.CreatePerson
{
    public class CreatePersonHandler : IRequestHandler<CreatePersonCommand>
    {
        private readonly IApplicationDbContext _context;

        private readonly ILogger<CreatePersonHandler> _logger;

        public CreatePersonHandler(IApplicationDbContext context, ILogger<CreatePersonHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Unit> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
        {
            var person = Person.Create(request.FirstName, request.LastName, request.PersonalNumber,
                request.BirthDate, request.GenderType, request.CityId);

            if (_context.Person.Any(x => x.PersonalNumber.Value == person.PersonalNumber.Value))
            {
                throw new ApplicationMessageException(ApplicationExceptionCode.PersonalNumberAlreadyExists);
            }

            foreach (var phoneNumber in request.PhoneNumbers.Select(x => PhoneNumber.Create(x.Value, x.TypeId, person.Id)))
            {
                if (_context.PhoneNumber.Any(x => x.Value == phoneNumber.Value))
                {
                    throw new ApplicationMessageException(ApplicationExceptionCode.PhoneNumberAlreadyExists);
                }

                person.PhoneNumbers.Add(phoneNumber);
            }

            await _context.Person.AddAsync(person, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            return await Task.FromResult(Unit.Value);
        }
    }
}
