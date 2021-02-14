using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;

namespace Application.PersonController.Queries.GetPersonInfo.Models
{
    public class GetPersonInfoRelationDto : IMapFrom<Relation>
    {
        public int Id { get; set; }

        public RelationTypeEnum TypeId { get; set; }

        public string TypeName { get; set; }

        public GetPersonInfoRelatedPersonInfoDto Person { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Relation, GetPersonInfoRelationDto>()
                .ForMember(d => d.TypeId, opt => opt.MapFrom(s => s.RelationTypeId))
                .ForMember(d => d.TypeName, opt => opt.MapFrom(s => s.RelationType.Name))
                .ForMember(d => d.Person, opt => opt.MapFrom(s => s.PersonTo));
        }
    }
}
