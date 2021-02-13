using System;
using System.Collections.Generic;
using Domain.Enums;
using MediatR;

namespace Application.PersonController.Commands.CreatePerson
{
    public class CreatePersonCommand : IRequest
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PersonalNumber { get; set; }

        public DateTime BirthDate { get; set; }

        public GenderTypeEnum GenderType { get; set; }

        public int CityId { get; set; }

        public List<CreatePersonPhoneNumber> PhoneNumbers { get; set; }

        public CreatePersonCommand()
        {
            PhoneNumbers = new List<CreatePersonPhoneNumber>();
        }

        public class CreatePersonPhoneNumber
        {
            public string Value { get; set; }

            public PhoneNumberTypeEnum TypeId { get; set; }
        }
    }
}
