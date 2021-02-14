using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.ReportController.Queries.GetReport.Models;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.ReportController.Queries.GetReport
{
    public class GetReportHandler : IRequestHandler<GetReportQuery, List<GetReportDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetReportHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<GetReportDto>> Handle(GetReportQuery request, CancellationToken cancellationToken)
        {
            var data = await _context.Person.Include(x => x.DirectRelatedPersons)
                .ThenInclude(x => x.RelationType)
                .Select(x => new
                {
                    x.Id,
                    x.Name.FirstName,
                    x.Name.LastName,
                    x.PersonalNumber,
                    RelationInfo = x.DirectRelatedPersons.Select(a => new
                    {
                        a.RelationTypeId,
                        RelationTypeName = a.RelationType.Name
                    })
                }).OrderBy(x => x.Id).AsNoTracking().ToListAsync(cancellationToken);

            return data.Select(x => new GetReportDto
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                PersonalNumber = x.PersonalNumber,
                RelationInfos = x.RelationInfo.GroupBy(a => new {a.RelationTypeId, a.RelationTypeName}).Select(a =>
                    new GetReportRelationInfoDto
                    {
                        RelationTypeId = a.Key.RelationTypeId,
                        RelationTypeName = a.Key.RelationTypeName,
                        TotalCount = a.Count()
                    })
            }).ToList();
        }
    }
}
