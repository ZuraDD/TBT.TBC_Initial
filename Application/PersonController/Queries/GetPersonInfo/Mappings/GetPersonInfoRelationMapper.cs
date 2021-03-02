using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Application.Common.Interfaces;
using Application.Common.Mappings;
using Application.PersonController.Queries.GetPersonInfo.Models;
using Domain.Entities;

namespace Application.PersonController.Queries.GetPersonInfo.Mappings
{
    public class GetPersonInfoRelationMapper : ICustomMapperInterface<Relation, GetPersonInfoRelationDto>, ICustomScopedMapperInterface
    {
        private readonly ICustomMapperInterface<Person, GetPersonInfoRelatedPersonInfoDto> _relatedPersonMapper;

        public GetPersonInfoRelationMapper(ICustomMapperInterface<Person, GetPersonInfoRelatedPersonInfoDto> relatedPersonMapper)
        {
            _relatedPersonMapper = relatedPersonMapper;
        }

        public GetPersonInfoRelationDto Map(Relation source)
        {
            return new GetPersonInfoRelationDto
            {
                Id = source.Id,
                TypeId = source.RelationTypeId,
                TypeName = source.RelationType.Name,
                Person = _relatedPersonMapper.Map(source.PersonTo)
            };
        }

        public Relation ReverseMap(GetPersonInfoRelationDto source)
        {
            throw new NotImplementedException();
        }
    }
}
