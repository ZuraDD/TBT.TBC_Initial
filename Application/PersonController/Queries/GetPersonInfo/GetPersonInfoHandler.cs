using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Enums;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Mappings;
using Application.PersonController.Queries.GetPersonInfo.Mappings;
using Application.PersonController.Queries.GetPersonInfo.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.PersonController.Queries.GetPersonInfo
{
    public class GetPersonInfoHandler : IRequestHandler<GetPersonInfoQuery, GetPersonInfoDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICustomMapperInterface<Person, GetPersonInfoDto> _customMapper;

        public GetPersonInfoHandler(IApplicationDbContext context, IMapper mapper, ICustomMapperInterface<Person, GetPersonInfoDto> customMapper)
        {
            _context = context;
            _mapper = mapper;
            _customMapper = customMapper;
        }

        public async Task<GetPersonInfoDto> Handle(GetPersonInfoQuery request, CancellationToken cancellationToken)
        {
            var data = await _context.Person.Where(x => x.Id == request.PersonId)
                .Include(x => x.City)
                .Include(x => x.GenderType)
                .Include(x => x.PhoneNumbers)
                    .ThenInclude(x => x.PhoneNumberType)

                .Include(x => x.DirectRelatedPersons)
                    .ThenInclude(x => x.RelationType)

                .Include(x => x.DirectRelatedPersons)
                    .ThenInclude(x => x.PersonTo)
                    .ThenInclude(x => x.City)

                .Include(x => x.DirectRelatedPersons)
                    .ThenInclude(x => x.PersonTo)
                    .ThenInclude(x => x.GenderType)

                .Include(x => x.DirectRelatedPersons)
                    .ThenInclude(x => x.PersonTo)
                    .ThenInclude(x => x.PhoneNumbers)
                    .ThenInclude(x => x.PhoneNumberType)

                .OrderBy(x => x.Id)
                .Select(x => _customMapper.Map(x))
                .AsNoTracking()
                .SingleOrDefaultAsync(cancellationToken);

            if (data == default(GetPersonInfoDto))
            {
                throw new ApplicationMessageException(ApplicationExceptionCode.PersonNotFound);
            }

            return data;
        }
    }
}
