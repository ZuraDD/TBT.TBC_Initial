using FluentValidation;

namespace Application.ReportController.Queries.GetReport
{
    public class GetReportValidator : AbstractValidator<GetReportQuery>
    {
        public GetReportValidator()
        {
        }
    }
}
