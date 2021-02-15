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

namespace Application.PersonController.Commands.UpdatePerson
{
    public class UpdatePersonHandler : IRequestHandler<UpdatePersonCommand>
    {
        private readonly IApplicationDbContext _context;

        private readonly ILogger<UpdatePersonHandler> _logger;

        public UpdatePersonHandler(IApplicationDbContext context, ILogger<UpdatePersonHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Unit> Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
        {
            var personNew = Person.Create(request.FirstName, request.LastName, request.PersonalNumber,
                request.BirthDate, request.GenderType, request.CityId);

            var person = await _context.Person.Include(x => x.PhoneNumbers).SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (person == default(Person))
            {
                throw new ApplicationMessageException(ApplicationExceptionCode.PersonNotFound);
            }

            if (await _context.Person.AnyAsync(x => x.Id != person.Id && x.PersonalNumber.Value == personNew.PersonalNumber.Value, cancellationToken))
            {
                throw new ApplicationMessageException(ApplicationExceptionCode.PersonalNumberAlreadyExists);
            }

            person.Update(personNew);

            var phoneNumbersNew = request.PhoneNumbers.Select(x => PhoneNumber.Create(x.Value, x.TypeId, person.Id)).ToList();

            foreach (var personPhoneNumber in person.PhoneNumbers.ToList().Where(personPhoneNumber => !phoneNumbersNew.Select(x => x.Value).Contains(personPhoneNumber.Value)))
            {
                personPhoneNumber.Delete();

                person.PhoneNumbers.Remove(personPhoneNumber);
            }

            foreach (var phoneNumber in phoneNumbersNew)
            {
                var existingNumber = person.PhoneNumbers.SingleOrDefault(x => x.Value == phoneNumber.Value);

                if (existingNumber != null)
                {
                    existingNumber.Update(phoneNumber);
                }
                else
                {
                    if (await _context.PhoneNumber.AnyAsync(x => x.Value == phoneNumber.Value, cancellationToken))
                    {
                        throw new ApplicationMessageException(ApplicationExceptionCode.PhoneNumberAlreadyExists);
                    }

                    person.PhoneNumbers.Add(phoneNumber);
                }
            }

            _context.Person.Update(person);

            await _context.SaveChangesAsync(cancellationToken);

            return await Task.FromResult(Unit.Value);
        }
    }
}
