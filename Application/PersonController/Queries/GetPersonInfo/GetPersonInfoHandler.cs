using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Enums;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.PersonController.Queries.GetPersonInfo.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.PersonController.Queries.GetPersonInfo
{
    public class GetPersonInfoHandler : IRequestHandler<GetPersonInfoQuery, GetPersonInfoDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetPersonInfoHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GetPersonInfoDto> Handle(GetPersonInfoQuery request, CancellationToken cancellationToken)
        {
            var data = await _context.Person.Where(x => x.Id == request.PersonId)
                .Include(x => x.City)
                .Include(x => x.GenderType)
                .Include(x => x.PhoneNumbers)
                .Include(x => x.DirectRelatedPersons)
                .ThenInclude(x => x.PersonTo)
                .AsNoTracking()
                .ProjectTo<GetPersonInfoDto>(_mapper.ConfigurationProvider)
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
