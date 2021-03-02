using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.Mappings;
using Application.Implementations.CustomServiceMapperImplementations;
using Application.PersonController.Queries.GetPersonList.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.PersonController.Queries.GetPersonList
{
    public class GetPersonListHandler : IRequestHandler<GetPersonListQuery, PaginatedList<GetPersonListDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly CustomPersonEntityServiceMapper _customPersonEntityServiceMapper;

        public GetPersonListHandler(IApplicationDbContext context, IMapper mapper, CustomPersonEntityServiceMapper customPersonEntityServiceMapper)
        {
            _context = context;
            _mapper = mapper;
            _customPersonEntityServiceMapper = customPersonEntityServiceMapper;
        }

        public async Task<PaginatedList<GetPersonListDto>> Handle(GetPersonListQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Person
                .Include(x => x.City)
                .Include(x => x.GenderType)
                .Where(x => string.IsNullOrWhiteSpace(request.FirstName) ||
                            EF.Functions.Like(x.Name.FirstName, $"%{request.FirstName}%"))
                .Where(x => string.IsNullOrWhiteSpace(request.LastName) ||
                            EF.Functions.Like(x.Name.LastName, $"%{request.LastName}%"))
                .Where(x => string.IsNullOrWhiteSpace(request.PersonalNumber) ||
                            EF.Functions.Like(x.PersonalNumber.Value, $"%{request.PersonalNumber}%"));

            if (request.IsAdvancedSearchEnabled)
                query = query.Where(x => !request.CityIds.Any() || request.CityIds.Contains(x.CityId))
                    .Where(x => !request.GenderTypeId.HasValue || x.GenderTypeId == request.GenderTypeId.Value)
                    .Where(x => !request.BirthDateFrom.HasValue || x.BirthDate.Value >= request.BirthDateFrom.Value)
                    .Where(x => !request.BirthDateTo.HasValue || x.BirthDate.Value <= request.BirthDateTo.Value);

            return await query.OrderBy(x => x.Id).Select(x => _customPersonEntityServiceMapper.MapToGetPersonListDto(x)).AsNoTracking()
                .PaginatedListAsync(request.PageNumber, request.PageSize);
        }
    }
}
