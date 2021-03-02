using System;
using System.Collections.Generic;
using System.IO;
using Application.Common.Mappings;
using Domain.Entities;
using Domain.Enums;

namespace Application.PersonController.Queries.GetPersonInfo.Models
{
    public class GetPersonInfoDto : IMapFrom<Person>
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PersonalNumber { get; set; }

        public DateTime BirthDate { get; set; }

        public string ImagePath { get; set; }

        public GenderTypeEnum? GenderTypeId { get; set; }

        public string GenderTypeName { get; set; }

        public int CityId { get; set; }
        
        public string CityName { get; set; }

        public IEnumerable<GetPersonInfoPhoneNumberDto> PhoneNumbers { get; set; }

        public IEnumerable<GetPersonInfoRelationDto> Relations { get; set; }

        public void Mapping(MappingProfile profile)
        {
            profile.CreateMap<Person, GetPersonInfoDto>()
                .ForMember(d => d.FirstName, opt => opt.MapFrom(s => s.Name.FirstName))
                .ForMember(d => d.LastName, opt => opt.MapFrom(s => s.Name.LastName))
                .ForMember(d => d.PersonalNumber, opt => opt.MapFrom(s => s.PersonalNumber.Value))
                .ForMember(d => d.BirthDate, opt => opt.MapFrom(s => s.BirthDate.Value))
                .ForMember(d => d.ImagePath, opt => opt.MapFrom(s => !string.IsNullOrWhiteSpace(s.ImagePath) ? Path.Combine(profile.PhotoUploadService.GetWebRootPath(), s.ImagePath) : null))
                .ForMember(d => d.GenderTypeId, opt => opt.MapFrom(s => s.GenderTypeId))
                .ForMember(d => d.GenderTypeName, opt => opt.MapFrom(s => s.GenderType.Name))
                .ForMember(d => d.CityId, opt => opt.MapFrom(s => s.CityId))
                .ForMember(d => d.CityName, opt => opt.MapFrom(s => s.City.Name))
                .ForMember(d => d.PhoneNumbers, opt => opt.MapFrom(s => s.PhoneNumbers))
                .ForMember(d => d.Relations, opt => opt.MapFrom(s => s.DirectRelatedPersons));
        }
    }
}
