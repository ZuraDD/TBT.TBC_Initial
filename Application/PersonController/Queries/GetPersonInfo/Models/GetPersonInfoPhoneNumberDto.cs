using System;
using System.Collections.Generic;
using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;

namespace Application.PersonController.Queries.GetPersonInfo.Models
{
    public class GetPersonInfoPhoneNumberDto : IMapFrom<PhoneNumber>
    {
        public int Id { get; set; }

        public PhoneNumberTypeEnum TypeId { get; set; }

        public string TypeName { get; set; }

        public string Value { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<PhoneNumber, GetPersonInfoPhoneNumberDto>()
                .ForMember(d => d.TypeId, opt => opt.MapFrom(s => s.PhoneNumberTypeId))
                .ForMember(d => d.TypeName, opt => opt.MapFrom(s => s.PhoneNumberType.Name));
        }
    }
}
