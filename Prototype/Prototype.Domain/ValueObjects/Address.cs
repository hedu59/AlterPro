﻿using Prototype.Domain.ValueObjects;
using System.Collections.Generic;

namespace Prototype.Domain.Entities
{
    public class Address   : ValueObject
    {

        public string Number { get; private set; }

        public string Complement { get; private set; }

        public string Neighborhood { get; private set; }

        public string Street { get; private set; }

        public string City { get; private set; }

        public string State { get; private set; }

        public string PostalCode { get; private set; }

        protected Address()
        {
        }

        private Address(

            string number,
            string street,
            string complement,
            string neighborhood,
            string city,
            string state,
            string postalCode)
        {
            Number = number;
            Street = street;
            Complement = complement;
            Neighborhood = neighborhood;
            City = city;
            State = state?.ToUpper();
            PostalCode = postalCode;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Number;
            yield return Street;
            yield return Complement;
            yield return Neighborhood;
            yield return City;
            yield return State;
            yield return PostalCode;
        }

        public static Address Create(
            string number,
            string street,
            string complement,
            string neighborhood,
            string city,
            string state,
            string postalCode
            )
        {
            return new Address(number,street, complement, neighborhood, city, state, postalCode);
        }
    }
}
