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
    public class GetPersonInfoRelatedPersonMapper : ICustomMapperInterface<Person, GetPersonInfoRelatedPersonInfoDto> , ICustomScopedMapperInterface
    {
        private readonly IPhotoUploadService _photoUploadService;

        public GetPersonInfoRelatedPersonMapper(IPhotoUploadService photoUploadService)
        {
            _photoUploadService = photoUploadService;
        }

        public GetPersonInfoRelatedPersonInfoDto Map(Person source)
        {
            return new GetPersonInfoRelatedPersonInfoDto
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
            };
        }

        public Person ReverseMap(GetPersonInfoRelatedPersonInfoDto source)
        {
            throw new NotImplementedException();
        }
    }
}
