using System;
using System.Collections.Generic;
using Domain.Enums;
using MediatR;

namespace Application.PersonController.Commands.UpdatePerson
{
    public class UpdatePersonCommand : IRequest
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PersonalNumber { get; set; }

        public DateTime BirthDate { get; set; }

        public GenderTypeEnum GenderType { get; set; }

        public int CityId { get; set; }

        public List<UpdatePersonPhoneNumber> PhoneNumbers { get; set; }

        public UpdatePersonCommand()
        {
            PhoneNumbers = new List<UpdatePersonPhoneNumber>();    
        }

        public class UpdatePersonPhoneNumber
        {
            public string Value { get; set; }

            public PhoneNumberTypeEnum TypeId { get; set; }
        }
    }
}
