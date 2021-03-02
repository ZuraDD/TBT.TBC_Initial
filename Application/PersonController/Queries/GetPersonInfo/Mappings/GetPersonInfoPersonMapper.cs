using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Application.Common.Interfaces;
using Application.Common.Mappings;
using Application.PersonController.Queries.GetPersonInfo.Models;
using Domain.Entities;

namespace Application.PersonController.Queries.GetPersonInfo.Mappings
{
    public class GetPersonInfoPersonMapper : ICustomMapperInterface<Person, GetPersonInfoDto> , ICustomScopedMapperInterface
    {
        private readonly IPhotoUploadService _photoUploadService;
        private readonly ICustomMapperInterface<PhoneNumber, GetPersonInfoPhoneNumberDto> _phoneNumberMapper;
        private readonly ICustomMapperInterface<Relation, GetPersonInfoRelationDto> _relationMapper;

        public GetPersonInfoPersonMapper(IPhotoUploadService photoUploadService, 
            ICustomMapperInterface<PhoneNumber, GetPersonInfoPhoneNumberDto> phoneNumberMapper,
            ICustomMapperInterface<Relation, GetPersonInfoRelationDto> relationMapper)
        {
            _photoUploadService = photoUploadService;
            _phoneNumberMapper = phoneNumberMapper;
            _relationMapper = relationMapper;
        }

        public GetPersonInfoDto Map(Person source)
        {
            return new GetPersonInfoDto
            {
                Id = source.Id,
                BirthDate = source.BirthDate,
                CityId = source.CityId,
                CityName = source.City.Name,
                FirstName = source.Name.FirstName,
                GenderTypeId = source.GenderTypeId,
                GenderTypeName = source.GenderType?.Name,
                LastName = source.Name.LastName,
                PersonalNumber = source.PersonalNumber,
                ImagePath = !string.IsNullOrWhiteSpace(source.ImagePath) ? Path.Combine(_photoUploadService.GetWebRootPath(), source.ImagePath) : null,
                PhoneNumbers = source.PhoneNumbers.Select(p => _phoneNumberMapper.Map(p)),
                Relations = source.DirectRelatedPersons.Select(r => _relationMapper.Map(r)),
            };
        }

        public Person ReverseMap(GetPersonInfoDto source)
        {
            throw new NotImplementedException();
        }
    }
}
