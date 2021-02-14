using Application.Common.Mappings;
using Domain.Entities;
using Domain.Enums;

namespace Application.ReportController.Queries.GetReport.Models
{
    public class GetReportRelationInfoDto
    {
        public RelationTypeEnum RelationTypeId { get; set; }

        public string RelationTypeName { get; set; }

        public int TotalCount { get; set; }
    }
}
