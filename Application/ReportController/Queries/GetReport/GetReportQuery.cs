using System.Collections.Generic;
using Application.ReportController.Queries.GetReport.Models;
using MediatR;

namespace Application.ReportController.Queries.GetReport
{
    public class GetReportQuery : IRequest<List<GetReportDto>>
    {

    }
}
