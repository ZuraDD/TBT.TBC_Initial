using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Application.Common.Interfaces;
using Application.Common.Mappings;
using Application.PersonController.Queries.GetPersonList.Models;
using Domain.Entities;

namespace Application.Implementations.CustomServiceMapperImplementations
{
    public class CustomPersonEntityServiceMapper : ICustomServiceMapperInterface<Person>, ICustomScopedMapperInterface
    {
        private readonly IPhotoUploadService _photoUploadService;

        public CustomPersonEntityServiceMapper(IPhotoUploadService photoUploadService)
        {
            _photoUploadService = photoUploadService;
        }

        public GetPersonListDto MapToGetPersonListDto(Person source)
        {
            return new GetPersonListDto
            {
                Id = source.Id,
                BirthDate = source.BirthDate.Value,
                CityId = source.CityId,
                CityName = source.City.Name,
                FirstName = source.Name.FirstName,
                LastName = source.Name.LastName,
                GenderTypeId = source.GenderTypeId,
                GenderTypeName = source.GenderType?.Name,
                PersonalNumber = source.PersonalNumber,
                ImagePath = !string.IsNullOrWhiteSpace(source.ImagePath) ? Path.Combine(_photoUploadService.GetWebRootPath(), source.ImagePath) : null
            };
        }
    }
}
