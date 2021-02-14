using System;
using System.Collections.Generic;
using System.IO;
using Application.Common.Mappings;
using Domain.Entities;
using Domain.Enums;

namespace Application.ReportController.Queries.GetReport.Models
{
    public class GetReportDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PersonalNumber { get; set; }

        public IEnumerable<GetReportRelationInfoDto> RelationInfos { get; set; }
    }
}
