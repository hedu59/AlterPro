using Prototype.Shared.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prototype.Domain.Entities
{
    public class Contact : Entity
    {
        [NotMapped]
        public virtual string FirstName { get; private set; }
        public string FullName { get; private set; }
        public string PhoneNumber { get; private set; }

        protected Contact()
        {
        }

        private Contact(string firstName, string fullName, string phoneNumber)
        {
            FirstName = firstName;
            FullName = fullName;
            PhoneNumber = phoneNumber;
        }

        public static Contact Create(string fullName, string phoneNumber)
        {
            var fullNameArray = fullName.Split();
            var firstName = String.Empty;

            if (fullNameArray.Length > 0)
                firstName = fullNameArray[0];

            return new Contact(firstName, fullName, phoneNumber);
        }

    }
}
