using System;
using System.Collections.Generic;
using Application.Common.Mappings;
using Application.PersonController.Queries.GetPersonList.Models;
using Domain.Enums;
using MediatR;

namespace Application.PersonController.Queries.GetPersonList
{
    public class GetPersonListQuery : IRequest<PaginatedList<GetPersonListDto>>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PersonalNumber { get; set; }

        public DateTime? BirthDateFrom { get; set; }

        public DateTime? BirthDateTo { get; set; }

        public GenderTypeEnum? GenderTypeId { get; set; }

        public ICollection<int> CityIds { get; set; }

        public bool IsAdvancedSearchEnabled { get; set; } = false;

        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 10;

        public GetPersonListQuery()
        {
            CityIds = new List<int>();
        }
    }
}
