using Microsoft.EntityFrameworkCore.Metadata;
using Prototype.Domain.Enums;
using Prototype.Domain.ValueObjects;
using Prototype.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prototype.Domain.Entities
{
    public class Invitation: Entity
    {
        private static decimal InitialPromotionalPrice() => 500;
        private static decimal DiscountPercentage() => 10;

        protected Invitation() { }

        public virtual long ContactId { get; private set; }
        public Contact Contact { get; private set; }
        public Address Address { get; private set; }
        public Email Email { get; private set; }
        public ECategory Category { get; private set; }
        public decimal Price { get; private set; }
        public string Description { get; private set; }
        public bool? Status { get; private set; }

        private Invitation(Contact contact, Address addess, Email email, ECategory category, decimal price, string description, bool? status)
        {
            Contact = contact;
            Address = addess;
            Email = email;
            Category = category;
            Price = price;
            Description = description;
            Status = status;
        }

        public static Invitation Create(Contact contact, Address addess, Email email, ECategory category, decimal price, string description, bool? status)
        =>new Invitation(contact, addess, email, category, price, description, status);

        public static Invitation Update(Contact contact, Address addess, Email email, ECategory category, decimal price, string description, bool? status)
        {
            price = CalculateDiscount(price);
            var invitation = new Invitation(contact, addess, email, category, price, description, status);
       
            return invitation;
        }

        public  void UpdateStatus(decimal price, bool status)
        {
            if (price > InitialPromotionalPrice() && status )
                price = CalculateDiscount(price);

            Status = status;
            Price = price;
        }

        private static decimal CalculateDiscount(decimal price)
        {
            var adjustedPrice = price - (price / 100 * 10);

            return adjustedPrice;
        }
    }
}
