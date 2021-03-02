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
    public class GetPersonInfoPhoneNumberMapper : ICustomMapperInterface<PhoneNumber, GetPersonInfoPhoneNumberDto>, ICustomSingletonMapperInterface
    {
        public GetPersonInfoPhoneNumberDto Map(PhoneNumber source)
        {
            return new GetPersonInfoPhoneNumberDto
            {
                Id = source.Id,
                TypeId = source.PhoneNumberType.Id,
                TypeName = source.PhoneNumberType.Name,
                Value = source.Value
            };
        }

        public PhoneNumber ReverseMap(GetPersonInfoPhoneNumberDto source)
        {
            throw new NotImplementedException();
        }
    }
}
